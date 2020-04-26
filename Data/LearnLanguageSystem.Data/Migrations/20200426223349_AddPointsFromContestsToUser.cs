using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLanguageSystem.Data.Migrations
{
    public partial class AddPointsFromContestsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsFromContests",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsFromContests",
                table: "AspNetUsers");
        }
    }
}
