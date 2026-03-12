using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingAvatarObjectToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentAvatarId",
                table: "Users",
                column: "CurrentAvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Avatars_CurrentAvatarId",
                table: "Users",
                column: "CurrentAvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Avatars_CurrentAvatarId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentAvatarId",
                table: "Users");
        }
    }
}
