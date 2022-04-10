using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Transportation.Api.Model;
using Transportation.Api.Settings;

namespace Transportation.Api.Extensions;

public static partial class StartupConfigurations
{

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        var databaseOptions = config.GetSection(DatabaseOptions.Config).Get<DatabaseOptions>();
        services.AddDbContext<transportationContext>(x => x.UseMySql(databaseOptions.ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.15-mariadb")));
        return services;
    }
}