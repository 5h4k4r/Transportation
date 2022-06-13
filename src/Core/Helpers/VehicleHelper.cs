using System.Text.Json;
using Core.Models.Responses;

namespace Api.Helpers;

public static class VehicleHelper
{
    public static PlaqueDtoResponse? PreparePlaque(string plaque)
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
            PropertyNameCaseInsensitive = true
        };
        var plaqueString = JsonSerializer.Serialize(plaque, serializationOptions);
        return plaqueString;
    }
}