using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.DataAccess.Migrations
{
    public partial class mig_stocktransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stocks",
                newName: "StockName");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserPortfolioId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UserPortfolioId",
                table: "Stocks",
                column: "UserPortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_UserPortfolios_UserPortfolioId",
                table: "Stocks",
                column: "UserPortfolioId",
                principalTable: "UserPortfolios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_UserPortfolios_UserPortfolioId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_UserPortfolioId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UserPortfolioId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "StockName",
                table: "Stocks",
                newName: "Name");
        }
    }
}
