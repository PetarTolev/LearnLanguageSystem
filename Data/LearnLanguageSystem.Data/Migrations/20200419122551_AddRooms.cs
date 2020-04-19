using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLanguageSystem.Data.Migrations
{
    public partial class AddRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contests_ContestId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ContestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContestId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ContestId",
                table: "Rooms",
                column: "ContestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ContestId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
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
    }
}
