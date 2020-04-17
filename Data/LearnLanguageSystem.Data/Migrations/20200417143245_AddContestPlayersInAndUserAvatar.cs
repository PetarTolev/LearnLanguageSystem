using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLanguageSystem.Data.Migrations
{
    public partial class AddContestPlayersInAndUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContestId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContestId",
                table: "AspNetUsers",
                column: "ContestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Contests_ContestId",
                table: "AspNetUsers",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contests_ContestId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ContestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContestId",
                table: "AspNetUsers");
        }
    }
}
