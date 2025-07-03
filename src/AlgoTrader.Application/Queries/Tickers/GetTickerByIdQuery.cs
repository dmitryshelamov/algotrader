using AlgoTrader.Application.Contracts;

using MediatR;

namespace AlgoTrader.Application.Queries.Tickers;

public sealed record GetTickerByIdQuery(Guid TickerId) : IRequest<TickerInternal?>;