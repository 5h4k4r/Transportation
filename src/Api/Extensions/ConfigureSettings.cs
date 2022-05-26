// using Api.Settings;

using Core.Settings;
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


        var cacheOptions = config.GetSection(CacheOptions.Config);

        services
            .AddOptions<CacheOptions>()
            .Bind(cacheOptions)
            .ValidateDataAnnotations();

        return services;
    }

    public static void ValidateOptions(this IServiceProvider sp)
    {
        _ = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
        _ = sp.GetRequiredService<IOptions<CacheOptions>>().Value;
    }
}