using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLanguageSystem.Data.Migrations
{
    public partial class AddNameToContests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contests");
        }
    }
}
