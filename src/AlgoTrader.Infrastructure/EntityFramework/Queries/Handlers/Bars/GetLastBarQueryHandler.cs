using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Application.Queries.Bars;
using AlgoTrader.Core.Entites.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework.Queries.Handlers.Bars;

internal sealed class GetLastBarQueryHandler : IRequestHandler<GetLastBarQuery, DateTime?>
{
    private readonly AlgoTraderDbContext _context;

    public GetLastBarQueryHandler(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public async Task<DateTime?> Handle(GetLastBarQuery request, CancellationToken cancellationToken)
    {
        BarInterval interval = request.Interval.ToDomain();
        DateTime? result = null;

        try
        {
            result = await _context.Bars
                .Where(x => x.Interval == interval && x.TickerId == request.TickerId)
                .MaxAsync(x => x.Date, cancellationToken);
        }
        catch (Exception e)
        {
            // ignored
        }

        return result;
    }
}