using AlgoTrader.Core.Entites;
using AlgoTrader.Core.Entites.Enums;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework;

public sealed class AlgoTraderDbContext : DbContext
{
    public DbSet<Ticker> Tickers { get; set; }
    public DbSet<Bar> Bars { get; set; }

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