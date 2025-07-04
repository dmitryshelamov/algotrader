using AlgoTrader.Core.Entities.Orders;
using AlgoTrader.Core.ValueObjects.Bots;
using AlgoTrader.Core.ValueObjects.Orders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlgoTrader.Infrastructure.EntityFramework.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                v => v.Value,
                v => new OrderId(v))
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(o => o.BotId)
            .HasConversion(
                v => v.Value,
                v => new BotId(v))
            .HasColumnName("bot_id");

        builder.OwnsOne(
            s => s.InitialData,
            data =>
            {
                data.Property(x => x.LimitQuantity)
                    .HasColumnName("initial_order_limit_quantity");
                data.Property(x => x.LimitPricePerAsset)
                    .HasColumnName("initial_order_limit_price_per_asset");
                data.Property(x => x.MarketFunds)
                    .HasColumnName("initial_order_market_funds");
                data.Property(x => x.MarketQuantity)
                    .HasColumnName("initial_order_market_quantity");
            });

        builder.OwnsOne(
            s => s.ExchangeData,
            data =>
            {
                data.Property(x => x.ExchangeOrderId)
                    .HasColumnName("exchange_order_id");
                data.Property(x => x.CreatedAt)
                    .HasColumnName("exchange_order_created");
                data.Property(x => x.ModifiedAt)
                    .HasColumnName("exchange_order_modified");
                data.Property(x => x.Quantity)
                    .HasColumnName("exchange_quantity");
                data.Property(x => x.Funds)
                    .HasColumnName("exchange_funds");
            });

        builder.Property(u => u.FeePercentage)
            .HasColumnName("fee_percentage")
            .ValueGeneratedNever();

        builder.Property(x => x.Direction).HasColumnName("direction").HasConversion<string>();
        builder.Property(x => x.Type).HasColumnName("type").HasConversion<string>();
        builder.Property(x => x.Status).HasColumnName("status").HasConversion<string>();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.ModifiedAt).HasColumnName("modified_at");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasConversion<string>();
    }
}