using Microsoft.EntityFrameworkCore;

namespace RetroRealm_Server.Models
{
    public class LogDatabaseContext : DbContext
    {
        public LogDatabaseContext(DbContextOptions<LogDatabaseContext> options) : base(options) { }
        public DbSet<Log> Logs { get; set; }
    }
}
