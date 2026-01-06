namespace RetroRealm_Server.DTOs
{
    public class CreateAchievementDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Requirement { get; set; }
        public int PrizeCoin { get; set; }
    }
}
