namespace RetroRealm_Server.DTOs.WebSocketDTOs
{
    public class WebSocketOutputDTO
    {
        public int? Id { get; set; }

        public string? UserName { get; set; }

        public string? Role { get; set; }

        public string? CharacterName { get; set; }

        public int? moveX { get; set; }

        public int? moveY { get; set; }

        public float posX { get; set; }
        public float posY { get; set; }

        public DateTime? LastTextDate { get; set; }

        public string? Text { get; set; }

        public DateTime LastSend { get; set; }
    }
}
