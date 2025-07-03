using AlgoTrader.Application.Services;
using AlgoTrader.Core.Repositories;
using AlgoTrader.Infrastructure.EntityFramework.Repositories;

namespace AlgoTrader.Infrastructure.EntityFramework;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AlgoTraderDbContext _context;
    public ITickerRepository TickerRepository { get; }
    public IBarRepository BarRepository { get; }

    public UnitOfWork(AlgoTraderDbContext context)
    {
        _context = context;
        TickerRepository = new TickerRepository(context);
        BarRepository = new BarRepository(context);
    }

    public async Task Complete(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}