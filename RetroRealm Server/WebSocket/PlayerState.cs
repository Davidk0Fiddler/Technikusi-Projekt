namespace RetroRealm_Server.WebSocket
{
    public class PlayerState
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string CharacterName { get; set; } = null!;

        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;

        public float posX { get; set; } = 320;
        public float posY { get; set; } = 320;

        public string? Text { get; set; }
        public DateTime? LastTextDate { get; set; }


        public DateTime LastSend { get; set; }
    }
}
