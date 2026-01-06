namespace RetroRealm_Server.DTOs
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
