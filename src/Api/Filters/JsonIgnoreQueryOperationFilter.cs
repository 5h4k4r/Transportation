using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Filters;

public class JsonIgnoreQueryOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        context.ApiDescription.ParameterDescriptions
            .Where(d => d.Source.Id == "Query").ToList()
            .ForEach(param =>
            {
                var toIgnore =
                    ((DefaultModelMetadata)param.ModelMetadata)
                    .Attributes.PropertyAttributes
                    ?.Any(x => x is JsonIgnoreAttribute);

                var toRemove = operation.Parameters
                    .FirstOrDefault(p => p.Name == param.Name);

                if (toIgnore ?? false)
                    operation.Parameters.Remove(toRemove);
            });
    }
}