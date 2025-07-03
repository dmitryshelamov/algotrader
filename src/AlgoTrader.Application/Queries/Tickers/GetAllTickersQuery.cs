using AlgoTrader.Application.Contracts;

using MediatR;

public sealed record GetAllTickersQuery : IRequest<List<TickerInternal>>;