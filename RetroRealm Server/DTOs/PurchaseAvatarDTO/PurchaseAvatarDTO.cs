using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs.PurchaseAvatarDTO
{
    public class PurchaseAvatarDTO
    {
        [Required]
        public string AvatarName { get; set; }
    }
}
