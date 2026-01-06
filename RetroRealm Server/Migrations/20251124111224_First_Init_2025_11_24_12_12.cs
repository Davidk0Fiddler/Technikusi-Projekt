using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations
{
    /// <inheritdoc />
    public partial class First_Init_2025_11_24_12_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achivements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achivements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bunny_Run_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaxDistance = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bunny_Run_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flappy_Bird_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaxPassedPipes = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flappy_Bird_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memory_Game_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinTime = table.Column<string>(type: "TEXT", nullable: false),
                    MinFlipping = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memory_Game_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Coins = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: true),
                    CurrentAvatarId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnedAvatarsId = table.Column<string>(type: "TEXT", nullable: false),
                    CompletedChallangesId = table.Column<string>(type: "TEXT", nullable: false),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: false),
                    MemoryCardStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    FlappyBirdStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    WordleStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    BunnyRunStatusId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Bunny_Run_Status_BunnyRunStatusId",
                        column: x => x.BunnyRunStatusId,
                        principalTable: "Bunny_Run_Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Flappy_Bird_Status_FlappyBirdStatusId",
                        column: x => x.FlappyBirdStatusId,
                        principalTable: "Flappy_Bird_Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Memory_Game_Status_MemoryCardStatusId",
                        column: x => x.MemoryCardStatusId,
                        principalTable: "Memory_Game_Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wordle_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompletedWords = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wordle_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wordle_Status_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bunny_Run_Status_UserId",
                table: "Bunny_Run_Status",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Flappy_Bird_Status_UserId",
                table: "Flappy_Bird_Status",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Memory_Game_Status_UserId",
                table: "Memory_Game_Status",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BunnyRunStatusId",
                table: "Users",
                column: "BunnyRunStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FlappyBirdStatusId",
                table: "Users",
                column: "FlappyBirdStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MemoryCardStatusId",
                table: "Users",
                column: "MemoryCardStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WordleStatusId",
                table: "Users",
                column: "WordleStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Wordle_Status_UserId",
                table: "Wordle_Status",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bunny_Run_Status_Users_UserId",
                table: "Bunny_Run_Status",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flappy_Bird_Status_Users_UserId",
                table: "Flappy_Bird_Status",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memory_Game_Status_Users_UserId",
                table: "Memory_Game_Status",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wordle_Status_WordleStatusId",
                table: "Users",
                column: "WordleStatusId",
                principalTable: "Wordle_Status",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bunny_Run_Status_Users_UserId",
                table: "Bunny_Run_Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Flappy_Bird_Status_Users_UserId",
                table: "Flappy_Bird_Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Memory_Game_Status_Users_UserId",
                table: "Memory_Game_Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Wordle_Status_Users_UserId",
                table: "Wordle_Status");

            migrationBuilder.DropTable(
                name: "Achivements");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bunny_Run_Status");

            migrationBuilder.DropTable(
                name: "Flappy_Bird_Status");

            migrationBuilder.DropTable(
                name: "Memory_Game_Status");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Wordle_Status");
        }
    }
}
