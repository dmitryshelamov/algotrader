using AlgoTrader.Core.Entities;
using AlgoTrader.Core.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlgoTrader.Infrastructure.EntityFramework.Configurations;

internal sealed class TickerConfiguration : IEntityTypeConfiguration<Ticker>
{
    public void Configure(EntityTypeBuilder<Ticker> builder)
    {
        builder.ToTable("tickers");
        builder.HasKey(x => x.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                v => v.Value,
                v => new TickerId(v))
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.OwnsOne(
            s => s.Symbol,
            settings =>
            {
                settings.Property(x => x.SymbolLeft)
                    .HasColumnName("ticker_left")
                    .IsRequired();
                settings.Property(x => x.SymbolRight)
                    .HasColumnName("ticker_right")
                    .IsRequired();
                settings.Ignore(x => x.FullSymbol);
            });

        builder.Property(u => u.MarketType)
            .HasColumnName("market_type")
            .HasConversion<string>();

        builder.Property(u => u.TickerStartDate).HasColumnName("ticker_start_date");
    }
}