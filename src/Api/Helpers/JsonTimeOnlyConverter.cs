using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Helpers;

public class JsonTimeOnlyConverter : JsonConverter<TimeOnly>
{

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToLongTimeString());
    }

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        TimeOnly.MaxValue.ToString("");
        return TimeOnly.Parse(reader.GetString() ?? "");
    }
}