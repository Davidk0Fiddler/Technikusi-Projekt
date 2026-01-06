using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroRealm_Server.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string LogType { get; set; }
        public string? Error { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int? UserId { get; set; }
    }
}
