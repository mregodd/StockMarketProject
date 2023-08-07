using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.DataAccess.Migrations
{
    public partial class InitialUpdate_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9b224eef-eda0-4466-9973-650ae15cc242");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d6a3520c-f1e8-464d-86f5-5b5a6cff91c4");
        }
    }
}
