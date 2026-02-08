using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockLens.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickers_Name",
                table: "Tickers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Industries_Name",
                table: "Industries",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickers_Name",
                table: "Tickers");

            migrationBuilder.DropIndex(
                name: "IX_Industries_Name",
                table: "Industries");
        }
    }
}
