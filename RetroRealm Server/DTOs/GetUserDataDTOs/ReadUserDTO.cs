using RetroRealm_Server.DTOs.BunnyRunDTOs;
using RetroRealm_Server.DTOs.FlappyBirdDTOs;
using RetroRealm_Server.DTOs.MemoryGameDTOs;
using RetroRealm_Server.DTOs.WorldeStatusDTOs;

namespace RetroRealm_Server.DTOs.GetUserDataDTOs
{
    public class ReadUserDTO
    {
        public string UserName { get; set; }
        public int Coins { get; set; }
        public string RoleName { get; set; }
        public int CurrentAvatarId { get; set; }
        public ReadBunnyRunStatusDTO BunnyRunStatus { get; set; }
        public ReadFlappyBirdStatusDTO FlappyBirdStatus { get; set;}
        public ReadMemoryGameStatusDTO MemoryGameStatus { get; set; }
        public ReadWordleStatusDTO WordleStatus { get; set; }
    }
}
