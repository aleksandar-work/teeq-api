using Microsoft.EntityFrameworkCore.Migrations;

namespace Teeq_Data.Migrations
{
    public partial class AddUsersFirebaseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FirebaseId",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirebaseId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Users",
                nullable: false);
        }
    }
}
