using AlgoTrader.Application.Contracts.Enums;

using MediatR;

namespace AlgoTrader.Application.Commands.Tickers.CreateTicker;

public sealed record CreateTickerCommand(
    string TickerLeft,
    string TickerRight,
    MarketTypeInternal MarketType,
    DateTime TickerStartDate) : IRequest<Guid>;