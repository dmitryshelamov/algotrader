using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.Repositories;
using AlgoTrader.Core.ValueObjects.Bots;

using Microsoft.EntityFrameworkCore;

namespace AlgoTrader.Infrastructure.EntityFramework.Repositories;

internal sealed class BotRepository : IBotRepository
{
    private readonly AlgoTraderDbContext _context;

    public BotRepository(AlgoTraderDbContext context)
    {
        _context = context;
    }

    public Task Add(LadderBot ladderBot, CancellationToken ct)
    {
        _context.Add(ladderBot);

        return Task.CompletedTask;
    }

    public async Task<LadderBot?> Get(BotId botId, CancellationToken ct)
    {
        return await _context.LadderBots.FirstOrDefaultAsync(x => x.Id == botId, ct);
    }
}