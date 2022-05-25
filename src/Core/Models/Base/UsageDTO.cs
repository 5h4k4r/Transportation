namespace Core.Models.Base;
public class UsageDto
{

    public ulong Id { get; set; }
    public string? StaticKey { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

}
