using AlgoTrader.Core.Entities;
using AlgoTrader.Core.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlgoTrader.Infrastructure.EntityFramework.Configurations;

internal sealed class BarConfiguration : IEntityTypeConfiguration<Bar>
{
    public void Configure(EntityTypeBuilder<Bar> builder)
    {
        builder.ToTable("bars");
        builder.HasKey(x => new { x.TickerId, x.Date, x.Interval });
        builder.Property(u => u.Date).HasColumnName("date");
        builder.Property(u => u.Open).HasColumnName("open");
        builder.Property(u => u.High).HasColumnName("high");
        builder.Property(u => u.Low).HasColumnName("low");
        builder.Property(u => u.Close).HasColumnName("close");
        builder.Property(u => u.Volume).HasColumnName("volume");
        builder.Property(u => u.Interval)
            .HasColumnName("interval")
            .HasConversion<string>();

        builder
            .HasOne<Ticker>(x => x.Ticker)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.TickerId, x.Interval, x.Date })
            .HasDatabaseName("idx_bar_symbol_interval_date")
            .IsDescending(true, true, true);

        builder.HasIndex(x => x.Date)
            .HasDatabaseName("idx_bar_date")
            .IsDescending(true);

        builder.Property(u => u.TickerId)
            .HasConversion(
                v => v.Value,
                v => new TickerId(v))
            .HasColumnName("ticker_id");
    }
}