using Api.Settings;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Extensions;

public static class StartupConfigurations
{

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var databaseOptions = config.GetSection(DatabaseOptions.Config);

        services
            .AddOptions<DatabaseOptions>()
            .Bind(databaseOptions)
            .ValidateDataAnnotations();

        services.AddDbContext<TransportationContext>((sp, b) =>
        {
            var options = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            b.UseMySql(options.ConnectionString, ServerVersion.Parse(options.ServerVersion));
        });
        return services;
    }

    public static void ValidateOptions(this IServiceProvider sp)
    {
        _ = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    }
}