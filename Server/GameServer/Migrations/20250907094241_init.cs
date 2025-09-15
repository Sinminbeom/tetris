using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameServer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerDbId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerDbId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomDbId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomDbId);
                });

            migrationBuilder.CreateTable(
                name: "RoomPlayer",
                columns: table => new
                {
                    RoomPlayerDbId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomDbId = table.Column<int>(type: "int", nullable: false),
                    PlayerDbId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPlayer", x => x.RoomPlayerDbId);
                    table.ForeignKey(
                        name: "FK_RoomPlayer_Player_PlayerDbId",
                        column: x => x.PlayerDbId,
                        principalTable: "Player",
                        principalColumn: "PlayerDbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomPlayer_Room_RoomDbId",
                        column: x => x.RoomDbId,
                        principalTable: "Room",
                        principalColumn: "RoomDbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerDbId",
                table: "Player",
                column: "PlayerDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomDbId",
                table: "Room",
                column: "RoomDbId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayer_PlayerDbId",
                table: "RoomPlayer",
                column: "PlayerDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayer_RoomDbId_PlayerDbId",
                table: "RoomPlayer",
                columns: new[] { "RoomDbId", "PlayerDbId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomPlayer");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
