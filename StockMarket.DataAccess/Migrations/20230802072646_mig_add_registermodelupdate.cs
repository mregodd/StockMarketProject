using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.DataAccess.Migrations
{
    public partial class mig_add_registermodelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "UserBalance",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_AppUserId",
                table: "User",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "UserBalance",
                table: "AspNetUsers");

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
    }
}
