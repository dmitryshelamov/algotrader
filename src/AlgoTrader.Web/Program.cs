using AlgoTrader.Infrastructure;
using AlgoTrader.Infrastructure.EntityFramework;

namespace AlgoTrader.Web;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        InfrastructureRegistrar.Configure(builder.Configuration, builder.Services);

        WebApplication app = builder.Build();
        app.MigrateToLatestVersion<AlgoTraderDbContext>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "TradeBot API"); });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}