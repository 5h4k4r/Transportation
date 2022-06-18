using System.Reflection;
using Api.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwaggerGenerator(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Transportation Api",
                    Version = "v3"
                });

            if (configuration.GetValue<string?>("BasePath", null) is { } serverUrl && !string.IsNullOrEmpty(serverUrl))
                c.AddServer(new OpenApiServer
                {
                    Url = serverUrl
                });

            //c.UseAllOfToExtendReferenceSchemas();
            c.SupportNonNullableReferenceTypes();
            c.UseDateOnlyTimeOnlyStringConverters();
            AddSecurity(c);
            AddXmlComments(c);
            AddOperationFilters(c);
            // Use method name as operationId
            c.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
        });

        return services;

        static void AddSecurity(SwaggerGenOptions c)
        {
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            c.AddSecurityDefinition(isDevelopment ? "user" : "auth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = isDevelopment ? "user" : "auth"
            });
        }

        static void AddXmlComments(SwaggerGenOptions c)
        {
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);
        }


        static void AddOperationFilters(SwaggerGenOptions c)
        {
            c.OperationFilter<JsonIgnoreQueryOperationFilter>();
            c.OperationFilter<SwaggerSecurityRequirementsOperationFilter>();
        }
    }
}