using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.ValueObjects.Bots;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlgoTrader.Infrastructure.EntityFramework.Configurations;

internal sealed class LadderBotConfiguration : IEntityTypeConfiguration<LadderBot>
{
    public void Configure(EntityTypeBuilder<LadderBot> builder)
    {
        builder.ToTable("ladder_bots");
        builder.HasKey(x => x.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                v => v.Value,
                v => new BotId(v))
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");
        builder.Property(x => x.ModifiedAt)
            .HasColumnName("modified_at");

        builder.Property(x => x.TickerId)
            .HasColumnName("ticker_id");

        builder.Property(u => u.Version)
            .HasColumnName("version");

        builder.OwnsOne(
            s => s.Settings,
            settings =>
            {
                settings.Property(x => x.LimitDeposit)
                    .HasColumnName("settings_limit_deposit")
                    .IsRequired();

                settings.Property(x => x.LimitPerOrder)
                    .HasColumnName("settings_limit_per_order")
                    .IsRequired();

                settings.Property(x => x.FallPercentage)
                    .HasColumnName("settings_fall_percent")
                    .IsRequired();

                settings.Property(x => x.ProfitPercentage)
                    .HasColumnName("settings_profit_percent")
                    .IsRequired();

                settings.Property(x => x.Taker)
                    .HasColumnName("settings_taker_fee")
                    .IsRequired();

                settings.Property(x => x.Maker)
                    .HasColumnName("settings_maker_fee")
                    .IsRequired();

                settings.Property(x => x.ReinvestmentProfits)
                    .HasColumnName("reinvestment_profits")
                    .IsRequired();

                settings.Property(x => x.TotalIncome)
                    .HasColumnName("total_income")
                    .IsRequired();
            });

        builder.HasMany(x => x.Orders)
            .WithOne()
            .HasForeignKey(o => o.BotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Ticker)
            .WithMany()
            .HasForeignKey(x => x.TickerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.ActiveOrders);
    }
}