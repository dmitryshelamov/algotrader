using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Core.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework.Queries.Handlers.Tickers;

internal sealed class GetAllTickersQueryHandler : IRequestHandler<GetAllTickersQuery, List<TickerInternal>>
{
    private readonly AlgoTraderDbContext _context;

    public GetAllTickersQueryHandler(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public async Task<List<TickerInternal>> Handle(GetAllTickersQuery request, CancellationToken cancellationToken)
    {
        List<Ticker> tickers = await _context.Tickers.ToListAsync(cancellationToken);

        return tickers.Select(x => x.ToInternal()).ToList();
    }
}