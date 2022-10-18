using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoringAppReact.Migrations
{
    public partial class partnershipchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partnership_Matches_MatchId",
                table: "Partnership");

            migrationBuilder.DropForeignKey(
                name: "FK_Partnership_Players_PlayerNotOutId",
                table: "Partnership");

            migrationBuilder.DropForeignKey(
                name: "FK_Partnership_Players_PlayerOutId",
                table: "Partnership");

            migrationBuilder.DropForeignKey(
                name: "FK_Partnership_Teams_TeamId",
                table: "Partnership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partnership",
                table: "Partnership");

            migrationBuilder.DropIndex(
                name: "IX_Partnership_PlayerNotOutId",
                table: "Partnership");

            migrationBuilder.DropIndex(
                name: "IX_Partnership_PlayerOutId",
                table: "Partnership");

            migrationBuilder.DropColumn(
                name: "PlayerNotOutBalls",
                table: "Partnership");

            migrationBuilder.DropColumn(
                name: "PlayerNotOutId",
                table: "Partnership");

            migrationBuilder.DropColumn(
                name: "PlayerNotOutRuns",
                table: "Partnership");

            migrationBuilder.DropColumn(
                name: "PlayerOutBalls",
                table: "Partnership");

            migrationBuilder.DropColumn(
                name: "PlayerOutRuns",
                table: "Partnership");

            migrationBuilder.DropColumn(
                name: "TotalRuns",
                table: "Partnership");

            migrationBuilder.RenameTable(
                name: "Partnership",
                newName: "Partnerships");

            migrationBuilder.RenameIndex(
                name: "IX_Partnership_TeamId",
                table: "Partnerships",
                newName: "IX_Partnerships_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Partnership_MatchId",
                table: "Partnerships",
                newName: "IX_Partnerships_MatchId");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerOutId",
                table: "Partnerships",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "Extras",
                table: "Partnerships",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Four",
                table: "Partnerships",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player1Balls",
                table: "Partnerships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player1Four",
                table: "Partnerships",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Player1Id",
                table: "Partnerships",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Player1Runs",
                table: "Partnerships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player1Six",
                table: "Partnerships",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Player2Balls",
                table: "Partnerships",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Player2Four",
                table: "Partnerships",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Player2Id",
                table: "Partnerships",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Player2Runs",
                table: "Partnerships",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Player2Six",
                table: "Partnerships",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Six",
                table: "Partnerships",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partnerships",
                table: "Partnerships",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partnerships_Player1Id",
                table: "Partnerships",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partnerships_Player2Id",
                table: "Partnerships",
                column: "Player2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partnerships_Matches_MatchId",
                table: "Partnerships",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partnerships_Players_Player1Id",
                table: "Partnerships",
                column: "Player1Id",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partnerships_Players_Player2Id",
                table: "Partnerships",
                column: "Player2Id",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partnerships_Teams_TeamId",
                table: "Partnerships",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partnerships_Matches_MatchId",
                table: "Partnerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Partnerships_Players_Player1Id",
                table: "Partnerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Partnerships_Players_Player2Id",
                table: "Partnerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Partnerships_Teams_TeamId",
                table: "Partnerships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partnerships",
                table: "Partnerships");

            migrationBuilder.DropIndex(
                name: "IX_Partnerships_Player1Id",
                table: "Partnerships");

            migrationBuilder.DropIndex(
                name: "IX_Partnerships_Player2Id",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Extras",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Four",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player1Balls",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player1Four",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player1Runs",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player1Six",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player2Balls",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player2Four",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player2Runs",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Player2Six",
                table: "Partnerships");

            migrationBuilder.DropColumn(
                name: "Six",
                table: "Partnerships");

            migrationBuilder.RenameTable(
                name: "Partnerships",
                newName: "Partnership");

            migrationBuilder.RenameIndex(
                name: "IX_Partnerships_TeamId",
                table: "Partnership",
                newName: "IX_Partnership_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Partnerships_MatchId",
                table: "Partnership",
                newName: "IX_Partnership_MatchId");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerOutId",
                table: "Partnership",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayerNotOutBalls",
                table: "Partnership",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlayerNotOutId",
                table: "Partnership",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PlayerNotOutRuns",
                table: "Partnership",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "PlayerOutBalls",
                table: "Partnership",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerOutRuns",
                table: "Partnership",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRuns",
                table: "Partnership",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partnership",
                table: "Partnership",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partnership_PlayerNotOutId",
                table: "Partnership",
                column: "PlayerNotOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Partnership_PlayerOutId",
                table: "Partnership",
                column: "PlayerOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partnership_Matches_MatchId",
                table: "Partnership",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partnership_Players_PlayerNotOutId",
                table: "Partnership",
                column: "PlayerNotOutId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partnership_Players_PlayerOutId",
                table: "Partnership",
                column: "PlayerOutId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partnership_Teams_TeamId",
                table: "Partnership",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
