using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıaddedproductTypeNewProdjj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "produckid",
                table: "ProductTypes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "produckid",
                table: "ProductTypes");
        }
    }
}
