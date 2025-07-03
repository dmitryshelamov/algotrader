using AlgoTrader.Application.Contracts;

using MediatR;

namespace AlgoTrader.Application.Queries;

public sealed record GetTickerBySymbolQuery(string SymbolLeft, string SymbolRight) : IRequest<TickerInternal?>;