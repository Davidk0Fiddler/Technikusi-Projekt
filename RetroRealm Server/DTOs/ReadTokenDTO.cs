using RetroRealm_Server.Models;

namespace RetroRealm_Server.DTOs
{
    public class ReadTokenDTO
    {
        public string Token { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}
