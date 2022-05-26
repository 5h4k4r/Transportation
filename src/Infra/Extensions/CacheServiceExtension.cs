using Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infra.Extensions;

public static class CacheServiceExtension
{
    public static IServiceCollection ConfigureCacheService(this IServiceCollection services, IConfiguration config)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            var cacheOptions = services.BuildServiceProvider().GetRequiredService<IOptions<CacheOptions>>().Value;
            options.Configuration = cacheOptions.ConnectionString;
            options.InstanceName = cacheOptions.InstanceName;
        });

        return services;
    }
}