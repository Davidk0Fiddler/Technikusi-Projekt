using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingAchievememts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEng",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "DescriptionEsp",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "DescriptionHun",
                table: "Achievements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEng",
                table: "Achievements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEsp",
                table: "Achievements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionHun",
                table: "Achievements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
