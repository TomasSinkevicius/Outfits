using Microsoft.EntityFrameworkCore.Migrations;

namespace Outfits.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HasInCart",
                table: "Product",
                defaultValue: "",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasInWishList",
                table: "Product",
                defaultValue: "",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasInCart",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "HasInWishList",
                table: "Product");
        }
    }
}
