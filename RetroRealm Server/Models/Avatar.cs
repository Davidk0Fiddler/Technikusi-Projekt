using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.Models
{
    public class Avatar
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
