using RetroRealm_Server.DTOs.BunnyRunDTOs;
using RetroRealm_Server.DTOs.FlappyBirdDTOs;
using RetroRealm_Server.DTOs.MemoryGameDTOs;
using RetroRealm_Server.DTOs.WorldeStatusDTOs;
using RetroRealm_Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroRealm_Server.DTOs.AdminPanelDTOs
{
    public class ReadUserForAdminDTO
    {
        public string Username { get; set; }
        public int Coins { get; set; }
        public string RoleName { get; set; }
        public string CurrentAvatarName { get; set; } 
        public List<string> OwnedAvatarsNames { get; set; }
        public List<string> CompletedChallangesName { get; set; }
        public ReadMemoryGameStatusDTO MemoryGameStatus { get; set; }
        public ReadFlappyBirdStatusDTO FlappyBirdStatus { get; set; }
        public ReadWordleStatusDTO WorldeStatus { get; set; }
        public ReadBunnyRunStatusDTO BunnyRunStatus { get; set; }
    }
}
