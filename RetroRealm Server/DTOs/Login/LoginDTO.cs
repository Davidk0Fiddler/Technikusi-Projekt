using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs.Login
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
