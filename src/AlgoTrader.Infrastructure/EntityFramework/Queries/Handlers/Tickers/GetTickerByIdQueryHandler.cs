using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Application.Queries.Tickers;
using AlgoTrader.Core.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework.Queries.Handlers.Tickers;

internal sealed class GetTickerByIdQueryHandler : IRequestHandler<GetTickerByIdQuery, TickerInternal?>
{
    private readonly AlgoTraderDbContext _context;

    public GetTickerByIdQueryHandler(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public async Task<TickerInternal?> Handle(GetTickerByIdQuery request, CancellationToken cancellationToken)
    {
        Ticker? ticker = await _context.Tickers.FirstOrDefaultAsync(x => x.Id == request.TickerId, cancellationToken);

        return ticker?.ToInternal();
    }
}