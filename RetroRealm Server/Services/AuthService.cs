using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetroRealm_Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly ILogger<AvatarService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(RetroRealmDatabaseContext context, ILogService logService, ILogger<AvatarService> logger, IConfiguration configuration, IUserService userService)
        {
            _context = context;
            _logService = logService;
            _logger = logger;
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
        public async Task<Result<ReadTokenDTO>> LoginAsync(LoginDTO model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Invalid credentials", DateTime.Now, null);
                return Result<ReadTokenDTO>.Fail("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Logged in! User:{model.Username}", DateTime.Now, null);
            return Result<ReadTokenDTO>.Ok(new ReadTokenDTO { Token = token, RefreshToken = refreshToken});
        }
        #endregion

        #region Refresh Token
        public async Task<Result<ReadTokenDTO>> RefreshTokenAsync(RefreshTokenDto model) {
            var refreshToken = await _context.RefreshTokens.Include(rt => rt.User).SingleOrDefaultAsync(rt => rt.Token == model.Token);
            if (refreshToken == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Invalid refresh token", DateTime.Now, null);
                return Result<ReadTokenDTO>.Fail("Invalid refresh token");
            }

            if (refreshToken.Expires < DateTime.UtcNow)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Expired refresh token", DateTime.Now, null);
                return Result<ReadTokenDTO>.Fail("Expired refresh token");
            }

            var newToken = GenerateJwtToken(refreshToken.User);
            var newRefreshToken = GenerateRefreshToken(refreshToken.User.Id);

            _context.RefreshTokens.Remove(refreshToken);
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Token refreshed! UserId - {newRefreshToken.UserId}", DateTime.Now, newRefreshToken.UserId);
            return Result<ReadTokenDTO>.Ok(new ReadTokenDTO { Token = newToken, RefreshToken = refreshToken });
        }
        #endregion

        #region Log out
        public async Task<Result<bool>> LogoutAsync(RefreshTokenDto model) {
            var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == model.Token);

            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }

            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"User {refreshToken.UserId} has logged out!", DateTime.Now, refreshToken.UserId);
            return Result<bool>.Ok(true);
        }
        #endregion

        #region generate Jwt Token
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
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

        public async Task<bool> CheckExpireDateAsync(RefreshTokenDto model)
        {
            var refreshToken = await _context.RefreshTokens.Include(rt => rt.User).SingleOrDefaultAsync(rt => rt.Token == model.Token);

            if (refreshToken.Expires < DateTime.UtcNow || refreshToken == null) return false;

            return true;

        }


        public async Task<int> GetUserIdFromRefreshTokenAsync(RefreshTokenDto model)
        {
            var refreshToken = await _context.RefreshTokens.Include(rt => rt.User).SingleOrDefaultAsync(rt => rt.Token == model.Token);
            return refreshToken.UserId;
        }

    }
};