using RetroRealm_Server.Models;

namespace RetroRealm_Server.DTOs
{
    public class ReadUserDTO
    {
        public string UserName { get; set; }
        public int Coins { get; set; }
        public string RoleName { get; set; }
        public int CurrentAvatarId { get; set; }
        public bool IsOnline { get; set; }
        public BunnyRunStatus BunnyRunStatus { get; set; }
        public FlappyBirdStatus FlappyBirdStatus { get; set;}
        public MemoryGameStatus MemoryGameStatus { get; set; }
        public WordleStatus WordleStatus { get; set; }
    }
}
