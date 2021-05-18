using Microsoft.EntityFrameworkCore.Migrations;

namespace Outfits.Data.Migrations
{
    public partial class Reccomendations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recommendation1",
                table: "OutfitPost",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recommendation2",
                table: "OutfitPost",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recommendation3",
                table: "OutfitPost",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recommendation1",
                table: "OutfitPost");

            migrationBuilder.DropColumn(
                name: "Recommendation2",
                table: "OutfitPost");

            migrationBuilder.DropColumn(
                name: "Recommendation3",
                table: "OutfitPost");
        }
    }
}
