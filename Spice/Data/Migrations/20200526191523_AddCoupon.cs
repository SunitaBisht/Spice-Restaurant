using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Data.Migrations
{
    public partial class AddCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumAmmount",
                table: "Coupon",
                newName: "MinimumAmount");

            migrationBuilder.AlterColumn<string>(
                name: "Spicyness",
                table: "MenuItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumAmount",
                table: "Coupon",
                newName: "MinimumAmmount");

            migrationBuilder.AlterColumn<string>(
                name: "Spicyness",
                table: "MenuItem",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
