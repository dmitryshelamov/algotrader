using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Enums;

using MediatR;

namespace AlgoTrader.Application.Queries.Bars;

public sealed record GetBarsAsStreamQuery(
    Guid TickerId,
    BarIntervalInternal Interval,
    DateTime From,
    DateTime To) : IStreamRequest<BarInternal>;