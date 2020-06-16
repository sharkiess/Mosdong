using Microsoft.EntityFrameworkCore.Migrations;

namespace Mosdong.Data.Migrations
{
    public partial class AddForeignKeyToMiniCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MiniCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MiniCategory_CategoryId",
                table: "MiniCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MiniCategory_Category_CategoryId",
                table: "MiniCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiniCategory_Category_CategoryId",
                table: "MiniCategory");

            migrationBuilder.DropIndex(
                name: "IX_MiniCategory_CategoryId",
                table: "MiniCategory");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MiniCategory");
        }
    }
}
