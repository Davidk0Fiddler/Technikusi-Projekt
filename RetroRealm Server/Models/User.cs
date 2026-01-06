using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace RetroRealm_Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int Coins { get; set; }  =  0;
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        [JsonIgnore]
        public Role? Role { get; set; }
        public int CurrentAvatarId { get; set; }
        public List<int> OwnedAvatarsId { get; set; }
        public List<int> CompletedChallangesId { get; set; }
        public bool IsOnline { get; set; }

        [JsonIgnore]
        public int? MemoryCardStatusId { get; set; }
        [ForeignKey("MemoryCardStatusId")]
        [JsonIgnore]
        public MemoryGameStatus? MemoryGameStatus { get; set; }

        [JsonIgnore]
        public int? FlappyBirdStatusId { get; set; }
        [ForeignKey("FlappyBirdStatusId")]
        [JsonIgnore]
        public FlappyBirdStatus? FlappyBirdStatus { get; set; }

        [JsonIgnore]
        public int? WordleStatusId { get; set; }
        [ForeignKey("WordleStatusId")]
        [JsonIgnore]
        public WordleStatus? WorldeStatus { get; set; }

        [JsonIgnore]
        public int? BunnyRunStatusId { get; set; }
        [ForeignKey("BunnyRunStatusId")]
        [JsonIgnore]
        public BunnyRunStatus? BunnyRunStatus { get; set;}

    }
}
