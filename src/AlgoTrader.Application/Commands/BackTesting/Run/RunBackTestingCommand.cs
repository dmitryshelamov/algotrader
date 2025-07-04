using AlgoTrader.Application.Contracts.Enums;

using MediatR;

namespace AlgoTrader.Application.Commands.BackTesting.Run;

public sealed record RunBackTestingCommand(Guid TickerId, BarIntervalInternal Interval, DateTime From, DateTime To) : IRequest;