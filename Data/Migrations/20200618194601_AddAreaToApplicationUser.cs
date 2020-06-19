using Microsoft.EntityFrameworkCore.Migrations;

namespace Mosdong.Data.Migrations
{
    public partial class AddAreaToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "AspNetUsers");
        }
    }
}
