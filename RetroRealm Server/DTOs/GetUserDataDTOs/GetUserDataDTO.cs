using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs.GetUserDataDTOs
{
    public class GetUserDataDTO
    {
        [Required]
        public string userName { get; set; }
    }
}
