using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoringAppReact.Migrations
{
    public partial class partnerShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partnership",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    MatchId = table.Column<long>(nullable: false),
                    TeamId = table.Column<long>(nullable: false),
                    WicketNo = table.Column<int>(nullable: false),
                    TotalRuns = table.Column<int>(nullable: false),
                    PlayerOutId = table.Column<long>(nullable: false),
                    PlayerOutRuns = table.Column<int>(nullable: false),
                    PlayerOutBalls = table.Column<int>(nullable: false),
                    PlayerNotOutId = table.Column<long>(nullable: false),
                    PlayerNotOutRuns = table.Column<long>(nullable: false),
                    PlayerNotOutBalls = table.Column<long>(nullable: false),
                    StartTime = table.Column<int>(nullable: true),
                    EndTime = table.Column<int>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partnership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partnership_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partnership_Players_PlayerNotOutId",
                        column: x => x.PlayerNotOutId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partnership_Players_PlayerOutId",
                        column: x => x.PlayerOutId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partnership_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partnership_MatchId",
                table: "Partnership",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Partnership_PlayerNotOutId",
                table: "Partnership",
                column: "PlayerNotOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Partnership_PlayerOutId",
                table: "Partnership",
                column: "PlayerOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Partnership_TeamId",
                table: "Partnership",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partnership");
        }
    }
}
