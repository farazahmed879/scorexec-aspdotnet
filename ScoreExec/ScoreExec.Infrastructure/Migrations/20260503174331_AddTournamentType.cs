using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreExec.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "tournaments");
        }
    }
}
