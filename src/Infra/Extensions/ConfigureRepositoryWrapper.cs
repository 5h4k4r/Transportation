using Core.Interfaces;
using Infra.Mapper;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class RepositoryWrapperExtension
{
    public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddTransient<IExceptionMapper, ExceptionMapper>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}