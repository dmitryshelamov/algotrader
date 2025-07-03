using AlgoTrader.Application.Commands.Jobs;
using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Enums;
using AlgoTrader.Infrastructure.BackgroundJobs.Configs;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlgoTrader.Infrastructure.BackgroundJobs;

internal sealed class BarUpdateBackgroundService : BackgroundService
{
    private readonly ILogger<BarUpdateBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IOptions<JobConfig> _config;

    public BarUpdateBackgroundService(ILogger<BarUpdateBackgroundService> logger, IServiceScopeFactory serviceScopeFactory, IOptions<JobConfig> config)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_config.Value.RunBarUpdateOnStartup)
        {
            _logger.LogInformation("BarUpdateBackgroundService disabled by config");

            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        List<TickerInternal> tickers = await mediator.Send(new GetAllTickersQuery(), stoppingToken);

        foreach (TickerInternal ticker in tickers)
        {
            foreach (BarIntervalInternal interval in Enum.GetValues(typeof(BarIntervalInternal)))
            {
                await mediator.Send(
                    new UpdateBarStorageCommand(
                        ticker.MarketType,
                        interval,
                        ticker.Id),
                    stoppingToken);
            }
        }
    }
}