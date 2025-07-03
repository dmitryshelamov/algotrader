using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Application.Queries;
using AlgoTrader.Application.Services;
using AlgoTrader.Core.Entites;
using AlgoTrader.Core.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace AlgoTrader.Application.Commands.Tickers.CreateTicker;

internal sealed class CreateTickerCommandHandler : IRequestHandler<CreateTickerCommand, Guid>
{
    private readonly ILogger<CreateTickerCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CreateTickerCommandHandler(
        ILogger<CreateTickerCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IMediator mediator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateTickerCommand request, CancellationToken ct)
    {
        var symbol = Symbol.Create(request.TickerLeft, request.TickerLeft);
        TickerInternal? ticker = await _mediator.Send(
            new GetTickerBySymbolQuery(
                request.TickerLeft,
                request.TickerRight),
            ct);

        if (ticker != null)
        {
            throw new Exception($"Ticker {symbol.FullSymbol} already exists");
        }

        var newTicker = Ticker.Create(
            Guid.NewGuid(),
            symbol,
            request.MarketType.ToDomain(),
            request.TickerStartDate);

        await _unitOfWork.Repository.Add(newTicker, ct);
        await _unitOfWork.Complete(ct);

        return newTicker.Id;
    }
}