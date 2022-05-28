using Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace Infra.Extensions;

public static class CacheServiceExtension
{
    public static IServiceCollection ConfigureCacheService(this IServiceCollection services, IConfiguration config)
    {
        var cacheOptions = services.BuildServiceProvider().GetRequiredService<IOptions<CacheOptions>>().Value;

        services.AddSingleton<IRedisClientsManagerAsync>(c =>
            new RedisManagerPool(cacheOptions.ConnectionString));


        return services;
    }
}