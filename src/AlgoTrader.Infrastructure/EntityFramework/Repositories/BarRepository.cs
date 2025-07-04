using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Repositories;

using EFCore.BulkExtensions;

namespace AlgoTrader.Infrastructure.EntityFramework.Repositories;

internal sealed class BarRepository : IBarRepository
{
    private readonly AlgoTraderDbContext _context;

    public BarRepository(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public Task AddRange(List<Bar> bars, CancellationToken ct)
    {
        _context.AddRange(bars);

        return Task.CompletedTask;
    }

    public async Task AddRangeAndSave(List<Bar> bars, CancellationToken ct)
    {
        await _context.BulkInsertOrUpdateAsync(bars, cancellationToken: ct);
    }
}