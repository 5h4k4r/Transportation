namespace Core.Models.Requests;

public class CreateTranslateFileRequest
{
    public ulong FileId { get; set; }
    public uint LanguageId { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string? Version { get; set; }
}