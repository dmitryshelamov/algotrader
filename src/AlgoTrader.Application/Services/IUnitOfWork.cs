using AlgoTrader.Core.Repositories;

namespace AlgoTrader.Application.Services;

public interface IUnitOfWork
{
    ITickerRepository Repository { get; }
    Task Complete(CancellationToken ct);
}