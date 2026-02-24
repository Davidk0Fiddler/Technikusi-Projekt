using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs.Register
{
    public class RegisterDTO
    {
        [Required, MaxLength(20, ErrorMessage = "The maximum length of the name can only be 20!"), MinLength(5, ErrorMessage = "The minimum length of the name should be at least 5!")]
        public string Username { get; set; }


        [Required, MaxLength(20, ErrorMessage = "The maximum length of the password can only be 20!"), MinLength(5, ErrorMessage = "The minimum length of the password should be at least 5!")]
        public string Password { get; set; }
    }
}
