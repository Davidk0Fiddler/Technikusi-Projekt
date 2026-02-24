using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Jwt_Service
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
        RefreshToken GenerateRefreshToken(int userId);
    }
}
