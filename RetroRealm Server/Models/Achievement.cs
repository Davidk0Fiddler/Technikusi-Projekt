using System.ComponentModel.DataAnnotations;

namespace RetroRealm_Server.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        public string NameEng { get; set; }
        public string NameEsp { get; set; }
        public string NameHun { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionEsp { get; set; }
        public string DescriptionHun { get; set; }
        public int GameId { get; set; }
        public int Requirement { get; set; }
        public int PrizeCoin { get; set; }
    }
}
