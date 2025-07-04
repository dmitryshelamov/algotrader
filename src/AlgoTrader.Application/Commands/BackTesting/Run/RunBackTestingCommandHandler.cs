using System.Diagnostics;

using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Queries.Bars;

using MediatR;

using Microsoft.Extensions.Logging;

namespace AlgoTrader.Application.Commands.BackTesting.Run;

internal sealed class RunBackTestingCommandHandler : IRequestHandler<RunBackTestingCommand>
{
    private readonly ILogger<RunBackTestingCommandHandler> _logger;
    private readonly IMediator _mediator;

    public RunBackTestingCommandHandler(ILogger<RunBackTestingCommandHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Handle(RunBackTestingCommand request, CancellationToken ct)
    {
        var time = Stopwatch.StartNew();
        var count = 0;

        _logger.LogInformation("BackTesting started");

        await foreach (BarInternal bar in _mediator.CreateStream(
                           new GetBarsAsStreamQuery(
                               request.TickerId,
                               request.Interval,
                               request.From,
                               request.To),
                           ct))
        {
            count++;
        }

        _logger.LogInformation("BackTesting ended, totalTime: {time}, count: {count}", time.Elapsed, count);
    }
}