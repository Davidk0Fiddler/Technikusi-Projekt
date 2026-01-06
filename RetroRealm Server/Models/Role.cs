using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RetroRealm_Server.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<User>? Users { get; set; }
    }
}
