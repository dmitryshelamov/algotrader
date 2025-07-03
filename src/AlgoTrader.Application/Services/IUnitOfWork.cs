using AlgoTrader.Core.Repositories;

namespace AlgoTrader.Application.Services;

public interface IUnitOfWork
{
    ITickerRepository TickerRepository { get; }
    IBarRepository BarRepository { get; }
    Task Complete(CancellationToken ct);
}