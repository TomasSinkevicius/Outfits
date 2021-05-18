using Microsoft.EntityFrameworkCore.Migrations;

namespace Outfits.Data.Migrations
{
    public partial class whohasliked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhoHasLiked",
                table: "Product",
                defaultValue: "",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhoHasLiked",
                table: "Product");
        }
    }
}
