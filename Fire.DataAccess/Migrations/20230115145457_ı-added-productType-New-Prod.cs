using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıaddedproductTypeNewProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsWhat",
                table: "ProductTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWhat",
                table: "ProductTypes");
        }
    }
}
