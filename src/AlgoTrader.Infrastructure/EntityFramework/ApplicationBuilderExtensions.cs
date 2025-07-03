using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlgoTrader.Infrastructure.EntityFramework;

public static class ApplicationBuilderExtensions
{
    public static void MigrateToLatestVersion<TContext>(this IApplicationBuilder app) where TContext : DbContext
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.Migrate();
    }
}