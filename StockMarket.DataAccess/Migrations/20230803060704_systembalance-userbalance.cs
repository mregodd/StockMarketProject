using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.DataAccess.Migrations
{
    public partial class systembalanceuserbalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ea0d9651-08a8-49a1-ae5e-aff484ab73e1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0c709156-b803-4495-9d6f-b0b5f53ff1db");
        }
    }
}
