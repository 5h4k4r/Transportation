using Core.Interfaces;
using Infra.Mapper;
using Core.Settings;
using Infra.Entities;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infra.Extensions;

public static class RepositoryWrapperExtension
{
    public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddTransient<IExceptionMapper, ExceptionMapper>();
        services.AddSingleton<ICurl, Curl>();
        services.AddDbContext<TransportationContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            dbContextOptionsBuilder.UseMySql(options.ConnectionString, ServerVersion.Parse(options.ServerVersion));
        });
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}