using Microsoft.EntityFrameworkCore.Migrations;

namespace Mosdong.Data.Migrations
{
    public partial class AddProductAdToProductItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductAd",
                table: "ProductItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAd",
                table: "ProductItem");
        }
    }
}
