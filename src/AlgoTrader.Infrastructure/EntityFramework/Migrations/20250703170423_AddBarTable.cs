using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoTrader.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddBarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:bar_interval", "one_minute,five_minutes,one_hour,one_day")
                .Annotation("Npgsql:Enum:market_type", "spot,futures")
                .OldAnnotation("Npgsql:Enum:market_type", "spot,futures");

            migrationBuilder.CreateTable(
                name: "bars",
                columns: table => new
                {
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ticker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    open = table.Column<decimal>(type: "numeric", nullable: false),
                    high = table.Column<decimal>(type: "numeric", nullable: false),
                    low = table.Column<decimal>(type: "numeric", nullable: false),
                    close = table.Column<decimal>(type: "numeric", nullable: false),
                    volume = table.Column<decimal>(type: "numeric", nullable: false),
                    interval = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bars", x => new { x.ticker_id, x.date });
                    table.ForeignKey(
                        name: "FK_bars_tickers_ticker_id",
                        column: x => x.ticker_id,
                        principalTable: "tickers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_bar_date",
                table: "bars",
                column: "date",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "idx_bar_symbol_interval_date",
                table: "bars",
                columns: new[] { "ticker_id", "interval", "date" },
                descending: new bool[0]);
            
            migrationBuilder.Sql(
                "SELECT create_hypertable('bars', 'date');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bars");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:market_type", "spot,futures")
                .OldAnnotation("Npgsql:Enum:bar_interval", "one_minute,five_minutes,one_hour,one_day")
                .OldAnnotation("Npgsql:Enum:market_type", "spot,futures");
        }
    }
}
