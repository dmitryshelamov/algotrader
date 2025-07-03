using AlgoTrader.Application.Commands.Tickers.CreateTicker;
using AlgoTrader.Application.Services;
using AlgoTrader.Core.Entites.Enums;
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
    }
}