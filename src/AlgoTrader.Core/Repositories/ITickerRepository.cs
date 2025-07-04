using AlgoTrader.Core.Entities;
using AlgoTrader.Core.ValueObjects;

namespace AlgoTrader.Core.Repositories;

public interface ITickerRepository
{
    Task Add(Ticker ticker, CancellationToken ct);
    Task<Ticker?> Get(TickerId tickerId, CancellationToken ct);
    Task<Ticker?> GetBySymbol(Symbol symbol, CancellationToken ct);
}