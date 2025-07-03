using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Application.Queries;
using AlgoTrader.Core.Entites;
using AlgoTrader.Core.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework.Queries.Handlers.Tickers;

internal sealed class GetTickerBySymbolQueryHandler : IRequestHandler<GetTickerBySymbolQuery, TickerInternal?>
{
    private readonly AlgoTraderDbContext _context;

    public GetTickerBySymbolQueryHandler(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public async Task<TickerInternal?> Handle(GetTickerBySymbolQuery request, CancellationToken cancellationToken)
    {
        var symbol = Symbol.Create(request.SymbolLeft, request.SymbolRight);
        Ticker? ticker = await _context.Tickers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Symbol == symbol, cancellationToken);

        return ticker?.ToInternal();
    }
}