using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Application.Queries.Bars;
using AlgoTrader.Application.Queries.Tickers;
using AlgoTrader.Application.Services;
using AlgoTrader.Core.Entities;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlgoTrader.Application.Commands.Jobs;

internal sealed class UpdateBarStorageCommandHandler : IRequestHandler<UpdateBarStorageCommand>
{
    private readonly ILogger<UpdateBarStorageCommandHandler> _logger;
    private readonly IExchangeRestClient _exchangeRestClient;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMediator _mediator;

    public UpdateBarStorageCommandHandler(
        ILogger<UpdateBarStorageCommandHandler> logger,
        IExchangeRestClient exchangeRestClient,
        IServiceScopeFactory serviceScopeFactory,
        IMediator mediator)
    {
        _logger = logger;
        _exchangeRestClient = exchangeRestClient;
        _serviceScopeFactory = serviceScopeFactory;
        _mediator = mediator;
    }

    public async Task Handle(UpdateBarStorageCommand request, CancellationToken ct)
    {
        TickerInternal? ticker = await _mediator.Send(new GetTickerByIdQuery(request.TickerId), ct);

        if (ticker == null)
        {
            throw new Exception($"No ticker found: ${request.TickerId}");
        }

        DateTime? lastDate = await _mediator.Send(new GetLastBarQuery(request.TickerId, request.BarInterval), ct);

        if (lastDate == null)
        {
            lastDate = ticker.TickerStartDate;
        }

        while (true)
        {
            List<BarInternal> newBars = await _exchangeRestClient.GetBars(
                ticker.FullSymbol,
                request.MarketType,
                request.BarInterval,
                lastDate,
                ct: ct);

            if (!newBars.Any())
            {
                _logger.LogInformation("No new bars found for {Symbol}", ticker.FullSymbol);

                break;
            }

            if (lastDate.HasValue)
            {
                BarInternal? toRemove = newBars.FirstOrDefault(x => x.Date == lastDate);

                if (toRemove != null)
                {
                    newBars.Remove(toRemove);
                }
            }

            if (!newBars.Any())
            {
                _logger.LogInformation("No new bars found for {Symbol}", ticker.FullSymbol);

                break;
            }

            Bar[] domains = newBars.Select(x => x.ToDomain(ticker.Id)).ToArray();

            lastDate = domains.Max(x => x.Date);

            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            {
                _logger.LogInformation(
                    "Interval: {interval}, range: from {fromDate} to {toDate}",
                    request.BarInterval,
                    domains.Min(x => x.Date),
                    domains.Max(x => x.Date));
                await unitOfWork.BarRepository.AddRangeAndSave(domains.ToList(), ct);
                await unitOfWork.Complete(ct);
            }
        }
    }
}