using AlgoTrader.Core.Repositories;

namespace AlgoTrader.Application.Services;

public interface IUnitOfWork
{
    ITickerRepository TickerRepository { get; }
    IBarRepository BarRepository { get; }
    IBotRepository BotRepository { get; }
    Task Complete(CancellationToken ct);
}