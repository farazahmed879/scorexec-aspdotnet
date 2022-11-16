using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoringAppReact.Migrations
{
    public partial class changeMatchdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiveStreaming",
                table: "MatchDetails");

            migrationBuilder.AddColumn<int>(
                name: "IsLiveOrMannual",
                table: "MatchDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiveOrMannual",
                table: "MatchDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsLiveStreaming",
                table: "MatchDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
