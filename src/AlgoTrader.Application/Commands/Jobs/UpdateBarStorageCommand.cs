using AlgoTrader.Application.Contracts.Enums;

using MediatR;

namespace AlgoTrader.Application.Commands.Jobs;

public sealed record UpdateBarStorageCommand(
    MarketTypeInternal MarketType,
    BarIntervalInternal BarInterval,
    Guid TickerId) : IRequest;