using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs
{
    public class RegisterDTO
    {
        [Required, MaxLength(20, ErrorMessage = "The max length of the name can only be 20!")]
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
