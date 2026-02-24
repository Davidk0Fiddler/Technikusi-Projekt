using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingMultiLanguageColsToAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Achievements",
                newName: "NameHun");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Achievements",
                newName: "NameEsp");

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

            migrationBuilder.AddColumn<string>(
                name: "NameEng",
                table: "Achievements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "NameEng",
                table: "Achievements");

            migrationBuilder.RenameColumn(
                name: "NameHun",
                table: "Achievements",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEsp",
                table: "Achievements",
                newName: "Description");
        }
    }
}
