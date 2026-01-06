using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations.LogDatabase
{
    /// <inheritdoc />
    public partial class RemoveExtraTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BunnyRunStatus");
            migrationBuilder.DropTable(name: "FlappyBirdStatus");
            migrationBuilder.DropTable(name: "MemoryGameStatus");
            migrationBuilder.DropTable(name: "Role");
            migrationBuilder.DropTable(name: "User");
            migrationBuilder.DropTable(name: "WordleStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
