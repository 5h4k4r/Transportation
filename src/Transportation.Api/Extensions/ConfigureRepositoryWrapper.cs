using Transportation.Api.Interfaces;
using Transportation.Api.Repositories;

namespace Transportation.Api.Extensions;

public static partial class RepositoryWrapperExtension
{
    public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
