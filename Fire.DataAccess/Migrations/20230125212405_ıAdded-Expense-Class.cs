using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıAddedExpenseClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "personSalaryControll",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "ExpensesPrice",
                table: "Expenses",
                newName: "Debt");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrFactory",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CustomerOrFactory",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "Debt",
                table: "Expenses",
                newName: "ExpensesPrice");

            migrationBuilder.AddColumn<bool>(
                name: "personSalaryControll",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
