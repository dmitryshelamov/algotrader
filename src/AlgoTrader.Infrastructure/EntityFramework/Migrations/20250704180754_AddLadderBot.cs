using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoTrader.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddLadderBot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "history_orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    trade_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticker = table.Column<string>(type: "text", nullable: false),
                    direction = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    initial_limit_quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    initial_limit_price_per_asset = table.Column<decimal>(type: "numeric", nullable: true),
                    initial_market_quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    initial_market_funds = table.Column<decimal>(type: "numeric", nullable: true),
                    exchange_order_id = table.Column<string>(type: "text", nullable: true),
                    exchange_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    exchange_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    exchange_quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    exchange_funds = table.Column<decimal>(type: "numeric", nullable: true),
                    fee_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    bot_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ladder_bots",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ticker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    settings_limit_deposit = table.Column<decimal>(type: "numeric", nullable: false),
                    settings_limit_per_order = table.Column<decimal>(type: "numeric", nullable: false),
                    settings_fall_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    settings_profit_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    settings_taker_fee = table.Column<decimal>(type: "numeric", nullable: false),
                    settings_maker_fee = table.Column<decimal>(type: "numeric", nullable: false),
                    reinvestment_profits = table.Column<bool>(type: "boolean", nullable: false),
                    total_income = table.Column<decimal>(type: "numeric", nullable: false),
                    version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ladder_bots", x => x.id);
                    table.ForeignKey(
                        name: "FK_ladder_bots_tickers_ticker_id",
                        column: x => x.ticker_id,
                        principalTable: "tickers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    direction = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    initial_order_limit_quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    initial_order_limit_price_per_asset = table.Column<decimal>(type: "numeric", nullable: true),
                    initial_order_market_quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    initial_order_market_funds = table.Column<decimal>(type: "numeric", nullable: true),
                    exchange_order_id = table.Column<string>(type: "text", nullable: true),
                    exchange_order_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    exchange_order_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    exchange_quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    exchange_funds = table.Column<decimal>(type: "numeric", nullable: true),
                    fee_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    bot_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_ladder_bots_bot_id",
                        column: x => x.bot_id,
                        principalTable: "ladder_bots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ladder_bots_ticker_id",
                table: "ladder_bots",
                column: "ticker_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_bot_id",
                table: "orders",
                column: "bot_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "history_orders");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "ladder_bots");
        }
    }
}
