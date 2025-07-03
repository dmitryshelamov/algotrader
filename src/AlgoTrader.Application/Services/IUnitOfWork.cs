using AlgoTrader.Core.Repositories;

namespace AlgoTrader.Application.Services;

public interface IUnitOfWork
{
    ITickerRepository TickerRepository { get; }
    Task Complete(CancellationToken ct);
}