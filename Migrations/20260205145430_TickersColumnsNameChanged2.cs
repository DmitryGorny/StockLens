using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockLens.Migrations
{
    /// <inheritdoc />
    public partial class TickersColumnsNameChanged2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Privaleged",
                table: "Tickers",
                newName: "Privileged");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Privileged",
                table: "Tickers",
                newName: "Privaleged");
        }
    }
}
