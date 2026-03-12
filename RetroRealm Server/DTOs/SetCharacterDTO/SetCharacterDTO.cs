using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs.SetCharacterDTO
{
    public class SetCharacterDTO
    {
        [Required]
        public string characterName { get; set; }
    }
}
