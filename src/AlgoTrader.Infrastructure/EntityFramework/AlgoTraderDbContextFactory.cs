using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlgoTrader.Infrastructure.EntityFramework;

internal sealed class AlgoTraderDbContextFactory : IDesignTimeDbContextFactory<AlgoTraderDbContext>
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=algotrader;Username=postgres;Password=postgres";

    public AlgoTraderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AlgoTraderDbContext>();
        optionsBuilder
            .UseNpgsql(ConnectionString);

        return new AlgoTraderDbContext(optionsBuilder.Options);
    }
}