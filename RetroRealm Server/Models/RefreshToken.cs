    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    namespace RetroRealm_Server.Models
    {
        public class RefreshToken
        {
            [JsonIgnore, Key]
            public int Id { get; set; }
            public string Token { get; set; }
            public DateTime Expires { get; set; }
            [JsonIgnore]
            public int UserId { get; set; }
            [ForeignKey("UserId"), JsonIgnore]
            public User User { get; set; }

        }
    }
