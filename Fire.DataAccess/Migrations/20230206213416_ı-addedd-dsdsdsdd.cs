using Microsoft.EntityFrameworkCore.Migrations;

namespace Fire.DataAccess.Migrations
{
    public partial class ıaddedddsdsdsdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_userRoles_userroleid",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_userroleid",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "userRolesid",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_userRolesid",
                table: "users",
                column: "userRolesid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_userRoles_userRolesid",
                table: "users",
                column: "userRolesid",
                principalTable: "userRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_userRoles_userRolesid",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_userRolesid",
                table: "users");

            migrationBuilder.DropColumn(
                name: "userRolesid",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "IX_users_userroleid",
                table: "users",
                column: "userroleid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_userRoles_userroleid",
                table: "users",
                column: "userroleid",
                principalTable: "userRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
