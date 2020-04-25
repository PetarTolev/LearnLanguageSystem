using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLanguageSystem.Data.Migrations
{
    public partial class AddEmailContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_ContestId",
                table: "Rooms");

            migrationBuilder.CreateTable(
                name: "EmailContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailContacts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ContestId",
                table: "Rooms",
                column: "ContestId",
                unique: true,
                filter: "[ContestId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailContacts");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ContestId",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ContestId",
                table: "Rooms",
                column: "ContestId");
        }
    }
}
