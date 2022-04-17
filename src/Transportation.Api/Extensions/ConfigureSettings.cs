using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Transportation.Api.Model;
using Transportation.Api.Settings;

namespace Transportation.Api.Extensions;

public static partial class StartupConfigurations
{

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var databaseOptions = config.GetSection(DatabaseOptions.Config);

        services
            .AddOptions<DatabaseOptions>()
            .Bind(databaseOptions)
            .ValidateDataAnnotations();

        services.AddDbContext<transportationContext>((sp, b) =>
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