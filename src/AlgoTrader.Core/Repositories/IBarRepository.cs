using AlgoTrader.Core.Entites;

namespace AlgoTrader.Core.Repositories;

public interface IBarRepository
{
    Task AddRange(IReadOnlyCollection<Bar> bars, CancellationToken ct);
}