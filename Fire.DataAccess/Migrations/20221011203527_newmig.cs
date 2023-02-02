using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class newmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_factoryQuantities_FactoryProductType_factoryproducttypeid",
                table: "factoryQuantities");

            migrationBuilder.DropIndex(
                name: "IX_factoryQuantities_factoryproducttypeid",
                table: "factoryQuantities");

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Kg",
                table: "productQuantity",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "alacak",
                table: "productQuantity",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "productQuantity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "paid",
                table: "productQuantity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Kg",
                table: "factoryQuantities",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "alacak",
                table: "factoryQuantities",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "factoryQuantities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "paid",
                table: "factoryQuantities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "personSalaryControll",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "branchid",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    İsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    İsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CheckCont",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    checkNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bankid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    checkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    whoFromGetted = table.Column<int>(type: "int", nullable: false),
                    toWhoWasGiven = table.Column<int>(type: "int", nullable: true),
                    branchid = table.Column<int>(type: "int", nullable: false),
                    İsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckCont", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usedid = table.Column<int>(type: "int", nullable: false),
                    debtprice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    giveprice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    İsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCont",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usedid = table.Column<int>(type: "int", nullable: false),
                    dailyTakeCount = table.Column<int>(type: "int", nullable: false),
                    totalkg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    totalpice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    branchid = table.Column<int>(type: "int", nullable: false),
                    paid = table.Column<bool>(type: "bit", nullable: false),
                    paidprice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    states = table.Column<bool>(type: "bit", nullable: false),
                    İsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCont", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StockTracking",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productid = table.Column<int>(type: "int", nullable: false),
                    totalkg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    branchid = table.Column<int>(type: "int", nullable: false),
                    İsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTracking", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "CheckCont");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentCont");

            migrationBuilder.DropTable(
                name: "StockTracking");

            migrationBuilder.DropColumn(
                name: "branchid",
                table: "users");

            migrationBuilder.DropColumn(
                name: "alacak",
                table: "productQuantity");

            migrationBuilder.DropColumn(
                name: "branchid",
                table: "productQuantity");

            migrationBuilder.DropColumn(
                name: "paid",
                table: "productQuantity");

            migrationBuilder.DropColumn(
                name: "Kg",
                table: "factoryQuantities");

            migrationBuilder.DropColumn(
                name: "alacak",
                table: "factoryQuantities");

            migrationBuilder.DropColumn(
                name: "branchid",
                table: "factoryQuantities");

            migrationBuilder.DropColumn(
                name: "paid",
                table: "factoryQuantities");

            migrationBuilder.DropColumn(
                name: "branchid",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "personSalaryControll",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "branchid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "branchid",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Kg",
                table: "productQuantity",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_factoryQuantities_factoryproducttypeid",
                table: "factoryQuantities",
                column: "factoryproducttypeid");

            migrationBuilder.AddForeignKey(
                name: "FK_factoryQuantities_FactoryProductType_factoryproducttypeid",
                table: "factoryQuantities",
                column: "factoryproducttypeid",
                principalTable: "FactoryProductType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
