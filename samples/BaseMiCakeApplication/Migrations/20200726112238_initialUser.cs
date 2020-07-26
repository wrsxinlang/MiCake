using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseMiCakeApplication.Migrations
{
    public partial class initialUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Itinerary",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Itinerary",
                newName: "ID");
        }
    }
}
