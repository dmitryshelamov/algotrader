using System.Runtime.CompilerServices;

using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Application.Queries.Bars;
using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Entities.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework.Queries.Handlers.Bars;

internal sealed class GetBarsAsStreamQueryHandler : IStreamRequestHandler<GetBarsAsStreamQuery, BarInternal>
{
    private readonly AlgoTraderDbContext _context;

    public GetBarsAsStreamQueryHandler(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<BarInternal> Handle(
        GetBarsAsStreamQuery request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        BarInterval interval = request.Interval.ToDomain();

        await foreach (Bar bar in _context.Bars
                           .AsNoTracking()
                           .Where(x => x.TickerId == request.TickerId
                               && x.Interval == interval
                               && x.Date >= request.From
                               && x.Date <= request.To)
                           .AsAsyncEnumerable()
                           .WithCancellation(cancellationToken))
        {
            yield return bar.ToInternal();
        }
    }
}