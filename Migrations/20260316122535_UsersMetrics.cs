using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockLens.Migrations
{
    /// <inheritdoc />
    public partial class UsersMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvestmentHorizon",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxDrawdownPercent",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReactionToDrop",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InvestmentHorizon",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaxDrawdownPercent",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReactionToDrop",
                table: "AspNetUsers");
        }
    }
}
