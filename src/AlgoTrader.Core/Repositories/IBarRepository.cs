using AlgoTrader.Core.Entities;

namespace AlgoTrader.Core.Repositories;

public interface IBarRepository
{
    Task AddRange(List<Bar> bars, CancellationToken ct);
    Task AddRangeAndSave(List<Bar> bars, CancellationToken ct);
}