namespace RetroRealm_Server.Models
{
    public class MemoryGameStatus : Status
    {
        public int[] MinTime { get; set; } = [999, 0, 0];
        public int MinFlipping { get; set; } = 0;
    }
}
