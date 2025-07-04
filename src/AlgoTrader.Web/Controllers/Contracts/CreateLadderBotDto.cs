using AlgoTrader.Application.Contracts;

namespace AlgoTrader.Web.Controllers.Contracts;

public sealed class CreateLadderBotDto
{
    public Guid TickerId { get; set; }
    public LadderBotSettingsInternal Settings { get; set; }
}