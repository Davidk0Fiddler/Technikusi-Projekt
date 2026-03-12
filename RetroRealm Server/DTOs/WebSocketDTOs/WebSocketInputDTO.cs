using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs.WebSocketDTOs
{
    public class WebSocketInputDTO
    {
        [Required]
        public string? UserName { get; set; }
        
        [Required]
        public string? Role { get; set; }

        [Required]
        public string? CharacterName { get; set; }
        
        [Required]
        public int? moveX { get; set; }

        [Required]
        public int? moveY { get; set; }

        [Required]
        public float posX { get; set; }

        [Required]
        public float posY { get; set; }

        [Required]
        public DateTime? LastTextDate { get; set; }

        [Required]
        public string? Text { get; set; }

        [Required]
        public DateTime LastSend { get; set; }
    }
}
