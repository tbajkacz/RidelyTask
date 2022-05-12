using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RidelyTask.Data.Context;

namespace RidelyTask.Data.Configuration;

public static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContextPool<RidelyDbContext>(cfg
            => cfg.UseNpgsql(configuration.GetConnectionString("Npgsql")));

    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RidelyDbContext>();

        dbContext.Database.Migrate();

        return builder;
    }
}