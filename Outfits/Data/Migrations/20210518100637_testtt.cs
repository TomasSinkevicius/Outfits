using Microsoft.EntityFrameworkCore.Migrations;

namespace Outfits.Data.Migrations
{
    public partial class testtt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Recommendation",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   OutfitPostId = table.Column<int>(nullable: false),
                   Recommendation1 = table.Column<string>(nullable: true),
                   Recommendation2 = table.Column<string>(nullable: true),
                   Recommendation3 = table.Column<string>(nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Recommendation", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
