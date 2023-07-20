using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.DataAccess.Migrations
{
    public partial class mig_add_relation_user_appuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AppUserID",
                table: "Users",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_AppUserID",
                table: "Users",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_AppUserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AppUserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Users");
        }
    }
}
