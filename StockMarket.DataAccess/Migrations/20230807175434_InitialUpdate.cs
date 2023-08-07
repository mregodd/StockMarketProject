using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.DataAccess.Migrations
{
    public partial class InitialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d6a3520c-f1e8-464d-86f5-5b5a6cff91c4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8e42ef72-e4ea-49b6-b5f5-60c009c0f700");
        }
    }
}
