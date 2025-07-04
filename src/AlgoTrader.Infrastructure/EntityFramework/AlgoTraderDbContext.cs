using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.Entities.Enums;
using AlgoTrader.Core.Entities.Orders;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework;

public sealed class AlgoTraderDbContext : DbContext
{
    public DbSet<Ticker> Tickers { get; set; }
    public DbSet<Bar> Bars { get; set; }
    public DbSet<LadderBot> LadderBots { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<HistoryOrder> HistoryOrders { get; set; }

    public AlgoTraderDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<MarketType>();
        modelBuilder.HasPostgresEnum<BarInterval>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlgoTraderDbContext).Assembly);
    }
}