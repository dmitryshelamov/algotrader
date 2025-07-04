using AlgoTrader.Application.Contracts;

using MediatR;

namespace AlgoTrader.Application.Commands.Bots.CreateLadderBot;

public sealed record CreateLadderBotCommand(Guid TickerId, LadderBotSettingsInternal Settings) : IRequest<Guid>;