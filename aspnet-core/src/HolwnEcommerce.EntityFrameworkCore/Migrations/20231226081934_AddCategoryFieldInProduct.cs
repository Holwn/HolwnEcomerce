using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolwnEcommerce.Migrations
{
    public partial class AddCategoryFieldInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "AppProducts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategorySlug",
                table: "AppProducts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CategorySlug",
                table: "AppProducts");
        }
    }
}
