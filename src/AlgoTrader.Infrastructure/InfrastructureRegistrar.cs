using AlgoTrader.Application.Commands.Tickers.CreateTicker;
using AlgoTrader.Application.Services;
using AlgoTrader.Core.Entities.Enums;
using AlgoTrader.Infrastructure.BackgroundJobs;
using AlgoTrader.Infrastructure.BackgroundJobs.Configs;
using AlgoTrader.Infrastructure.CryptoClientsNet.ByBitExchange;
using AlgoTrader.Infrastructure.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace AlgoTrader.Infrastructure;

public static class InfrastructureRegistrar
{
    public static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        services.AddSerilog((serviceProvider, lc) => lc
            .ReadFrom.Configuration(configuration));

        services.AddDbContext<AlgoTraderDbContext>((_, options) =>
        {
            options.UseNpgsql(
                    configuration.GetConnectionString("DbContext"),
                    x =>
                    {
                        x.MapEnum<MarketType>();
                        x.MapEnum<BarInterval>();
                    })
                .EnableSensitiveDataLogging(false);
        });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateTickerCommand>();
            cfg.RegisterServicesFromAssemblyContaining<AlgoTraderDbContext>();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExchangeRestClient, ByBitExchangeRestClient>();
        services.Configure<JobConfig>(configuration.GetSection("JobConfig"));
        services.AddHostedService<BarUpdateBackgroundService>();
    }
}