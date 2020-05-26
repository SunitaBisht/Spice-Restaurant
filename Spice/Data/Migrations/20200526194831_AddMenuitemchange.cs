using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Data.Migrations
{
    public partial class AddMenuitemchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "MenuItem",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "MenuItem",
                newName: "Discription");
        }
    }
}
