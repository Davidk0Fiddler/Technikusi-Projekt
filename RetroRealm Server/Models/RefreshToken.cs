using System.ComponentModel.DataAnnotations.Schema;

namespace RetroRealm_Server.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
