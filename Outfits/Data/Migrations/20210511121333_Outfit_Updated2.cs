using Microsoft.EntityFrameworkCore.Migrations;

namespace Outfits.Data.Migrations
{
    public partial class Outfit_Updated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Product1",
                table: "OutfitPost",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product2",
                table: "OutfitPost",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product3",
                table: "OutfitPost",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product1",
                table: "OutfitPost");

            migrationBuilder.DropColumn(
                name: "Product2",
                table: "OutfitPost");

            migrationBuilder.DropColumn(
                name: "Product3",
                table: "OutfitPost");
        
        }
    }
}
