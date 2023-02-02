using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıaddedCheckNewProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWhat",
                table: "CheckCont",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWhat",
                table: "CheckCont");
        }
    }
}
