using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoringAppReact.Migrations
{
    public partial class fallofwicketupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eight",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Fifth",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "First",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Fourth",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Ninth",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Second",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Seventh",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Sixth",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Tenth",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Third",
                table: "FallOfWickets");

            migrationBuilder.AddColumn<int>(
                name: "EndTime",
                table: "FallOfWickets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayerId",
                table: "FallOfWickets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Runs",
                table: "FallOfWickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartTime",
                table: "FallOfWickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WicketNo",
                table: "FallOfWickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FallOfWickets_PlayerId",
                table: "FallOfWickets",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FallOfWickets_Players_PlayerId",
                table: "FallOfWickets",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FallOfWickets_Players_PlayerId",
                table: "FallOfWickets");

            migrationBuilder.DropIndex(
                name: "IX_FallOfWickets_PlayerId",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "Runs",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "FallOfWickets");

            migrationBuilder.DropColumn(
                name: "WicketNo",
                table: "FallOfWickets");

            migrationBuilder.AddColumn<int>(
                name: "Eight",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fifth",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "First",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fourth",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ninth",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Second",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Seventh",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sixth",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tenth",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Third",
                table: "FallOfWickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
