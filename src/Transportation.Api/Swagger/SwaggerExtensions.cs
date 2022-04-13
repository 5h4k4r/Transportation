using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Transportation.Api.Swagger;

namespace Transportation.Api.Swagger;

public static partial class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwaggerGenerator(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                            new OpenApiInfo
                            {
                                Title = "Transportation Api",
                                Version = "v3"
                            });
            if (configuration.GetValue<string?>("BasePath", null) is string serverUrl && !string.IsNullOrEmpty(serverUrl))
            {
                c.AddServer(new OpenApiServer()
                {
                    Url = serverUrl
                });
            }

            //c.UseAllOfToExtendReferenceSchemas();
            c.SupportNonNullableReferenceTypes();

            // AddSecurity(c);
            AddXmlComments(c);
            AddOperationFilters(c);

            // Use method name as operationId
            c.CustomOperationIds(apiDesc =>
        {
            return apiDesc.TryGetMethodInfo(out System.Reflection.MethodInfo methodInfo) ? methodInfo.Name : null;
        });


        });

        return services;


        static void AddXmlComments(SwaggerGenOptions c)
        {
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (System.IO.File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);
        }


        static void AddOperationFilters(SwaggerGenOptions c)
        {
            c.OperationFilter<SwaggerFileUploadOperationFilter>();
            c.OperationFilter<SwaggerSecurityRequirementsOperationFilter>();
        }
    }
}