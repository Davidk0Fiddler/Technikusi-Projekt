using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.DTOs.Login;
using RetroRealm_Server.DTOs.Register;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services._NotUserServices.Interfaces;
using RetroRealm_Server.Services.LogService;
using RetroRealm_Server.Services.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RetroRealm_Server.Services._NotUserServices
{
    public class AuthService : IAuthService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(RetroRealmDatabaseContext context, ILogService logService, IConfiguration configuration, IUserService userService)
        {
            _context = context;
            _logService = logService;
            _configuration = configuration;
            _userService = userService;
        }

        #region Register
        public async Task<Result<string>> RegisterAsync(RegisterDTO model) {
            var result = await _userService.CreateNewUserAsync(model);

            if (result.Error == "User already exists") return Result<string>.Fail("User already exists");

            if (result.Error == "Database error") return Result<string>.Fail("Database error");

            if (result.Error == "Error") return Result<string>.Fail("Error");

            var addingresult = await _userService.AddStatusesToUserAsync(result.Data.Username);

            if (addingresult.Error == "Error during creating statuses!") return Result<string>.Fail("Error during creating statuses!");

            if (addingresult.Error == "Database error") return Result<string>.Fail("Database error");

            if (addingresult.Error == "Error") return Result<string>.Fail("Error");

            return Result<string>.Ok("User registered successfully");
        }
        #endregion

        #region login
        public async Task<Result<OutputTokenDTO>> LoginAsync(LoginDTO model)
        {
            var user = await _context.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Username == model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Invalid credentials", DateTime.Now, null);
                return Result<OutputTokenDTO>.Fail("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Logged in! User:{model.Username}", DateTime.Now, null);
            return Result<OutputTokenDTO>.Ok(new OutputTokenDTO { Token = token, RefreshToken = refreshToken});
        }
        #endregion


        // NOTE:
        // In production, refresh token reuse detection and device binding
        // should be implemented for higher security.
        // This implementation is sufficient for educational purposes.

        #region Refresh Token
        public async Task<Result<OutputTokenDTO>> RefreshTokenAsync(RefreshTokenDto model) {
            var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == model.Token);
            if (refreshToken == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Invalid refresh token", DateTime.Now, null);
                return Result<OutputTokenDTO>.Fail("Invalid refresh token");
            }

            if (refreshToken.Expires < DateTime.UtcNow)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Expired refresh token", DateTime.Now, null);
                return Result<OutputTokenDTO>.Fail("Expired refresh token");
            }

            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == refreshToken.UserId);

            var newToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken(refreshToken.User.Id);

            _context.RefreshTokens.Remove(refreshToken);
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Token refreshed! UserId - {newRefreshToken.UserId}", DateTime.Now, newRefreshToken.UserId);
            return Result<OutputTokenDTO>.Ok(new OutputTokenDTO { Token = newToken, RefreshToken = newRefreshToken });
        }
        #endregion

        #region Log out
        public async Task<Result<bool>> LogoutAsync(RefreshTokenDto model) {
            var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == model.Token);

            if (refreshToken != null)
            {
                await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"User {refreshToken.UserId} has logged out!", DateTime.Now, refreshToken.UserId);
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }

            return Result<bool>.Ok(true);
        }
        #endregion

        #region generate Jwt Token
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpireMinutes")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region generate refresh token
        private RefreshToken GenerateRefreshToken(int userId)
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:RefreshExpireMinutes")),
                UserId = userId
            };

        }
        #endregion

        #region delete all refresh tokens
        public async Task<Result<bool>> DeleteAllRefreshTokenns() {
            _context.RefreshTokens.RemoveRange(_context.RefreshTokens);
            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
        #endregion
    }
};