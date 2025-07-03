using AlgoTrader.Application.Contracts;

using MediatR;

namespace AlgoTrader.Application.Queries.Tickers;

public sealed record GetTickerBySymbolQuery(string SymbolLeft, string SymbolRight) : IRequest<TickerInternal?>;