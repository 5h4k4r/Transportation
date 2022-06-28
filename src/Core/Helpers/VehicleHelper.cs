using System.Text.Json;
using Core.Models.Responses;

namespace Core.Helpers;

public static class VehicleHelper
{
    private static readonly SnakeCaseNamingPolicy SnakeCasePolicy = new();

    public static PlaqueDtoResponse? PreparePlaque(string? plaque)
    {
        var deserializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var plaqueJson = JsonSerializer.Deserialize<PlaqueDtoResponse>(plaque, deserializationOptions);
        return plaqueJson;
    }

    public static string? PlaqueToString(PlaqueDtoResponse plaque)
    {
        var serializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = SnakeCasePolicy
        };
        var plaqueString = JsonSerializer.Serialize(plaque, serializationOptions);
        return plaqueString;
    }
}