using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockLens.Migrations
{
    /// <inheritdoc />
    public partial class Briefcasesandmanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Briefcases",
                columns: table => new
                {
                    BriefcasesId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Briefcases", x => x.BriefcasesId);
                    table.ForeignKey(
                        name: "FK_Briefcases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BriefcasesTickers",
                columns: table => new
                {
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    BriefcaseId = table.Column<int>(type: "integer", nullable: false),
                    percantage = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BriefcasesTickers", x => new { x.BriefcaseId, x.TickerId });
                    table.ForeignKey(
                        name: "FK_BriefcasesTickers_Briefcases_BriefcaseId",
                        column: x => x.BriefcaseId,
                        principalTable: "Briefcases",
                        principalColumn: "BriefcasesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BriefcasesTickers_Tickers_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Tickers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Briefcases_UserId",
                table: "Briefcases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BriefcasesTickers_TickerId",
                table: "BriefcasesTickers",
                column: "TickerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BriefcasesTickers");

            migrationBuilder.DropTable(
                name: "Briefcases");
        }
    }
}
