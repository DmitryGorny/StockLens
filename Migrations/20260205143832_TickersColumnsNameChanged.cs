using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockLens.Migrations
{
    /// <inheritdoc />
    public partial class TickersColumnsNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Privalaged",
                table: "Tickers",
                newName: "Privaleged");

            migrationBuilder.AlterColumn<decimal>(
                name: "DividendsPercents",
                table: "Tickers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Privaleged",
                table: "Tickers",
                newName: "Privalaged");

            migrationBuilder.AlterColumn<double>(
                name: "DividendsPercents",
                table: "Tickers",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
