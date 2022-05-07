using Transportation.Api.Model;

namespace Tranportation.Api.Responses;

public class LanguageResponse
{
    public uint? Id { get; set; }
    public string? Title { get; set; } = null!;
    public string Locale { get; set; } = null!;
    public string? Direction { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public LanguageResponse(Language language)
    {
        Id = language.Id;
        Title = language.Title;
        Locale = language.Locale;
        Direction = language.Direction;
        CreatedAt = language.CreatedAt;
        UpdatedAt = language.UpdatedAt;

    }
}