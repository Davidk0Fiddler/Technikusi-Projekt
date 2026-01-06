using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm_Server.Migrations.LogDatabase
{
    /// <inheritdoc />
    public partial class LogV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_User_UserId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_UserId",
                table: "Logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BunnyRunStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxDistance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BunnyRunStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlappyBirdStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPassedPipes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlappyBirdStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemoryGameStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinFlipping = table.Column<int>(type: "INTEGER", nullable: false),
                    MinTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryGameStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BunnyRunStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    FlappyBirdStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    MemoryCardStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: true),
                    WordleStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    Coins = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedChallangesId = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentAvatarId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnedAvatarsId = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_BunnyRunStatus_BunnyRunStatusId",
                        column: x => x.BunnyRunStatusId,
                        principalTable: "BunnyRunStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_FlappyBirdStatus_FlappyBirdStatusId",
                        column: x => x.FlappyBirdStatusId,
                        principalTable: "FlappyBirdStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_MemoryGameStatus_MemoryCardStatusId",
                        column: x => x.MemoryCardStatusId,
                        principalTable: "MemoryGameStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WordleStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedWords = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordleStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordleStatus_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BunnyRunStatus_UserId",
                table: "BunnyRunStatus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlappyBirdStatus_UserId",
                table: "FlappyBirdStatus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryGameStatus_UserId",
                table: "MemoryGameStatus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BunnyRunStatusId",
                table: "User",
                column: "BunnyRunStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_FlappyBirdStatusId",
                table: "User",
                column: "FlappyBirdStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MemoryCardStatusId",
                table: "User",
                column: "MemoryCardStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WordleStatusId",
                table: "User",
                column: "WordleStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WordleStatus_UserId",
                table: "WordleStatus",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_User_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BunnyRunStatus_User_UserId",
                table: "BunnyRunStatus",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlappyBirdStatus_User_UserId",
                table: "FlappyBirdStatus",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemoryGameStatus_User_UserId",
                table: "MemoryGameStatus",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_WordleStatus_WordleStatusId",
                table: "User",
                column: "WordleStatusId",
                principalTable: "WordleStatus",
                principalColumn: "Id");
        }
    }
}
