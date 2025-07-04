using AlgoTrader.Application.Contracts.Converters;
using AlgoTrader.Application.Services;
using AlgoTrader.Core.Entities.Bots;

using MediatR;

namespace AlgoTrader.Application.Commands.Bots.CreateLadderBot;

internal sealed class CreateLadderBotCommandHandler : IRequestHandler<CreateLadderBotCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLadderBotCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateLadderBotCommand request, CancellationToken cancellationToken)
    {
        var bot = LadderBot.Create(Guid.NewGuid(), request.TickerId, DateTime.UtcNow, request.Settings.ToDomain());

        await _unitOfWork.BotRepository.Add(bot, cancellationToken);
        await _unitOfWork.Complete(cancellationToken);

        return bot.Id;
    }
}