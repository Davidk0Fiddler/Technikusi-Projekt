using RetroRealm_Server.Models;

namespace RetroRealm_Server.DTOs.Login
{
    public class OutputTokenDTO
    {
        public string Token { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}
