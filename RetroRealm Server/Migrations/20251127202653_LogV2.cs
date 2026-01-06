using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations
{
    /// <inheritdoc />
    public partial class LogV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionMessage",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "ExceptionStackTrace",
                table: "Logs",
                newName: "Error");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Error",
                table: "Logs",
                newName: "ExceptionStackTrace");

            migrationBuilder.AddColumn<string>(
                name: "ExceptionMessage",
                table: "Logs",
                type: "TEXT",
                nullable: true);
        }
    }
}
