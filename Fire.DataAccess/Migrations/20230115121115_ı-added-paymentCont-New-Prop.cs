using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıaddedpaymentContNewProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWhat",
                table: "PaymentCont",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWhat",
                table: "PaymentCont");
        }
    }
}
