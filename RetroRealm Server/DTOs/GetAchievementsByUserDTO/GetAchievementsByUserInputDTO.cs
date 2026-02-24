using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.DTOs.GetAchievementsByUser
{
    public class GetAchievementsByUserInputDTO
    {
        [Required]
        public string UserName { get; set; }
    }
}
