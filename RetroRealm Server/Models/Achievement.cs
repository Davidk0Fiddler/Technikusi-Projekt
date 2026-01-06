using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Requirement { get; set; }
        public int PrizeCoin { get; set; }
    }
}
