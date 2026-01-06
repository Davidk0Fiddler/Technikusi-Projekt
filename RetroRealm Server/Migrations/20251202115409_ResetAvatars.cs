using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations
{
    /// <inheritdoc />
    public partial class ResetAvatars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Avatars;");

            // AUTOINCREMENT reset
            migrationBuilder.Sql("DELETE FROM sqlite_sequence WHERE name = 'Avatars';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
