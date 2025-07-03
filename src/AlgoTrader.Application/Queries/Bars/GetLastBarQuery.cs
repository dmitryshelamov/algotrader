using AlgoTrader.Application.Contracts.Enums;

using MediatR;

namespace AlgoTrader.Application.Queries.Bars;

public sealed record GetLastBarQuery(Guid TickerId, BarIntervalInternal Interval) : IRequest<DateTime?>;