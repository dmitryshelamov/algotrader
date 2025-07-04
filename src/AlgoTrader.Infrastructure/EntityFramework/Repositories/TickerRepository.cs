using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Repositories;
using AlgoTrader.Core.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework.Repositories;

internal sealed class TickerRepository : ITickerRepository
{
    private readonly AlgoTraderDbContext _context;

    public TickerRepository(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public Task Add(Ticker ticker, CancellationToken ct)
    {
        _context.Add(ticker);

        return Task.CompletedTask;
    }

    public async Task<Ticker?> Get(TickerId tickerId, CancellationToken ct)
    {
        return await _context.Tickers.FirstOrDefaultAsync(x => x.Id == tickerId, ct);
    }

    public Task<Ticker?> GetBySymbol(Symbol symbol, CancellationToken ct)
    {
        return _context.Tickers.FirstOrDefaultAsync(x => x.Symbol == symbol, ct);
    }
}