using Microsoft.EntityFrameworkCore;
namespace RetroRealm_Server.Models

{

    public class RetroRealmDatabaseContext : DbContext

    {

        public RetroRealmDatabaseContext(DbContextOptions<RetroRealmDatabaseContext> options) : base(options) { }


        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<BunnyRunStatus> Bunny_Run_Status { get; set; }
        public DbSet<FlappyBirdStatus> Flappy_Bird_Status { get; set; }
        public DbSet<MemoryGameStatus> Memory_Game_Status { get; set; }
        public DbSet<WordleStatus> Wordle_Status { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
        }
    }

}