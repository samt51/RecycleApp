using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class factoryUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "branchid",
                table: "factoryQuantities");

            migrationBuilder.DropColumn(
                name: "dailyTakeCount",
                table: "factoryQuantities");

            migrationBuilder.RenameColumn(
                name: "factoryid",
                table: "factoryQuantities",
                newName: "ReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "factoryQuantities",
                newName: "factoryid");

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "factoryQuantities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dailyTakeCount",
                table: "factoryQuantities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
