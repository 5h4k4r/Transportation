using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Swagger;

public class SwaggerSecurityRequirementsOperationFilter : IOperationFilter
{

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        HandleAuthorizeAttribute(operation, context);
    }

    private static void HandleAuthorizeAttribute(OpenApiOperation operation, OperationFilterContext context)
    {
        var declaringType = context.MethodInfo.DeclaringType;

        if (declaringType is null) return;

        var hasAuthorizeAttribute = declaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                                    context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (!hasAuthorizeAttribute) return;

        var hasAllowAnonymousAttribute = context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

        if (hasAllowAnonymousAttribute)
            return;

        if (!operation.Responses.ContainsKey("401"))
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        if (!operation.Responses.ContainsKey("403"))
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });


        var userHeaderAuth = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "user" }
        };

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new() {
                [ userHeaderAuth ] = Array.Empty<string>()
            }
        };
    }

}
