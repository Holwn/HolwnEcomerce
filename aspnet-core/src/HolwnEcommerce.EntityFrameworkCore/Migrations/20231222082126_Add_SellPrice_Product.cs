using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolwnEcommerce.Migrations
{
    public partial class Add_SellPrice_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SellPrice",
                table: "AppProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "AppProducts");
        }
    }
}
