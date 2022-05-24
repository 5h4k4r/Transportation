namespace Transportation.Api.Auth;

using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Region
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class Language
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("iso")]
    public string Iso { get; set; } = string.Empty;

    [JsonPropertyName("__v")]
    public int V { get; set; }

    [JsonPropertyName("direction")]
    public string Direction { get; set; } = string.Empty;
}

public class TokenObject
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("serviceId")]
    public string ServiceId { get; set; } = string.Empty;

    [JsonPropertyName("deviceType")]
    public string DeviceType { get; set; } = string.Empty;

    [JsonPropertyName("expirationTime")]
    public DateTime ExpirationTime { get; set; }

    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
}

public class AuthUser
{
    [JsonPropertyName("region")]
    public Region? Region { get; set; }

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("mobile")]
    public string Mobile { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public Language? Language { get; set; }

    [JsonPropertyName("readonly")]
    public bool Readonly { get; set; }

    [JsonPropertyName("roles")]
    public List<object>? Roles { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("token")]
    public TokenObject? Token { get; set; }
    [JsonIgnore]
    public IEnumerable<byte>? RoleUsers { get; set; }
}

