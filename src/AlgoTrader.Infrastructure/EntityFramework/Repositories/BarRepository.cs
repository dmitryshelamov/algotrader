using AlgoTrader.Core.Entites;
using AlgoTrader.Core.Repositories;

namespace AlgoTrader.Infrastructure.EntityFramework.Repositories;

internal sealed class BarRepository : IBarRepository
{
    private readonly AlgoTraderDbContext _context;

    public BarRepository(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public Task AddRange(IReadOnlyCollection<Bar> bars, CancellationToken ct)
    {
        _context.AddRange(bars);

        return Task.CompletedTask;
    }
}