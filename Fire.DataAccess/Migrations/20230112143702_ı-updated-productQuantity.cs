using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıupdatedproductQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "branchid",
                table: "productQuantity");

            migrationBuilder.DropColumn(
                name: "customerid",
                table: "productQuantity");

            migrationBuilder.RenameColumn(
                name: "dailyTakeCount",
                table: "productQuantity",
                newName: "ReceiptId");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "productQuantity",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "productQuantity");

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "productQuantity",
                newName: "dailyTakeCount");

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "productQuantity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "customerid",
                table: "productQuantity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
