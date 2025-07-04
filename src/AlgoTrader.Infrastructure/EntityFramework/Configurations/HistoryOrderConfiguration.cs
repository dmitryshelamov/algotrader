using AlgoTrader.Core.Entities.Orders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlgoTrader.Infrastructure.EntityFramework.Configurations;

internal sealed class HistoryOrderConfiguration : IEntityTypeConfiguration<HistoryOrder>
{
    public void Configure(EntityTypeBuilder<HistoryOrder> builder)
    {
        builder.ToTable("history_orders");

        builder.HasKey(o => o.Id);
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(o => o.TradeId)
            .HasColumnName("trade_id");
        builder.Property(o => o.Ticker)
            .HasColumnName("ticker");
        builder.Property(o => o.Status)
            .HasColumnName("status")
            .HasConversion<string>();
        builder.Property(o => o.Type)
            .HasColumnName("type")
            .HasConversion<string>();
        builder.Property(o => o.Direction)
            .HasColumnName("direction")
            .HasConversion<string>();
        builder.Property(o => o.CreatedAt)
            .HasColumnName("created_at");
        builder.Property(o => o.ModifiedAt)
            .HasColumnName("modified_at");
        builder.Property(o => o.InitialLimitQuantity)
            .HasColumnName("initial_limit_quantity");
        builder.Property(o => o.InitialLimitPricePerAsset)
            .HasColumnName("initial_limit_price_per_asset");
        builder.Property(o => o.InitialMarketQuantity)
            .HasColumnName("initial_market_quantity");
        builder.Property(o => o.InitialMarketFunds)
            .HasColumnName("initial_market_funds");
        builder.Property(o => o.ExchangeOrderId)
            .HasColumnName("exchange_order_id");
        builder.Property(o => o.ExchangeCreatedAt)
            .HasColumnName("exchange_created_at");
        builder.Property(o => o.ExchangeModifiedAt)
            .HasColumnName("exchange_modified_at");
        builder.Property(o => o.ExchangeQuantity)
            .HasColumnName("exchange_quantity");
        builder.Property(o => o.ExchangeFunds)
            .HasColumnName("exchange_funds");
        builder.Property(o => o.FeePercentage)
            .HasColumnName("fee_percentage");
        builder.Property(o => o.BotId)
            .HasColumnName("bot_id");
    }
}