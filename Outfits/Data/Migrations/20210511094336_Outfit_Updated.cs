using Microsoft.EntityFrameworkCore.Migrations;

namespace Outfits.Data.Migrations
{
    public partial class Outfit_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OutfitPostId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_OutfitPostId",
                table: "Product",
                column: "OutfitPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_OutfitPost_OutfitPostId",
                table: "Product",
                column: "OutfitPostId",
                principalTable: "OutfitPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_OutfitPost_OutfitPostId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_OutfitPostId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OutfitPostId",
                table: "Product");
        }
    }
}
