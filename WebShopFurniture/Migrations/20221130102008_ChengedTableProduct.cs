using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopFurniture.Migrations
{
    public partial class ChengedTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvailableProduct",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableProduct",
                table: "Products");
        }
    }
}
