namespace Transportation.Api.Auth;

using System.Text.Json.Serialization;
// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Region
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Language
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("iso")]
    public string Iso { get; set; }

    [JsonPropertyName("__v")]
    public int V { get; set; }

    [JsonPropertyName("direction")]
    public string Direction { get; set; }
}

public class TokenObject
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("serviceId")]
    public string ServiceId { get; set; }

    [JsonPropertyName("deviceType")]
    public string DeviceType { get; set; }

    [JsonPropertyName("expirationTime")]
    public DateTime ExpirationTime { get; set; }

    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
}

public class AuthUser
{
    [JsonPropertyName("region")]
    public Region Region { get; set; }

    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("mobile")]
    public string Mobile { get; set; }

    [JsonPropertyName("language")]
    public Language Language { get; set; }

    [JsonPropertyName("readonly")]
    public bool Readonly { get; set; }

    [JsonPropertyName("roles")]
    public List<object> Roles { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    [JsonPropertyName("token")]
    public TokenObject Token { get; set; }
}

