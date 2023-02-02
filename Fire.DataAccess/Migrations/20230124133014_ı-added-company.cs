using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıaddedcompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "branchid",
                table: "Customers",
                newName: "companyId");

            migrationBuilder.AddColumn<int>(
                name: "companyId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "companyId",
                table: "factories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "companyId",
                table: "Branch",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "companyId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "companyId",
                table: "factories");

            migrationBuilder.DropColumn(
                name: "companyId",
                table: "Branch");

            migrationBuilder.RenameColumn(
                name: "companyId",
                table: "Customers",
                newName: "branchid");
        }
    }
}
