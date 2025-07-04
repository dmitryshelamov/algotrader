using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.ValueObjects.Bots;

namespace AlgoTrader.Core.Repositories;

public interface IBotRepository
{
    Task Add(LadderBot ladderBot, CancellationToken ct);
    Task<LadderBot?> Get(BotId botId, CancellationToken ct);
}