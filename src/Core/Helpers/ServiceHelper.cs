using System.Text.Json;
using Core.Models.Requests;

namespace Core.Helpers;

public class ServiceHelper
{
    private static readonly SnakeCaseNamingPolicy SnakeCasePolicy = new();

    public static ServiceAreaTypeParams? PrepareParams(string serviceParams)
    {
        var deserializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var response = JsonSerializer.Deserialize<ServiceAreaTypeParams>(serviceParams, deserializationOptions);
        return response;
    }

    public static string PrepareResponse(ServiceAreaTypeParams serviceParams)
    {
        var serializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = SnakeCasePolicy
        };
        var response = JsonSerializer.Serialize(serviceParams, serializationOptions);
        return response;
    }
}