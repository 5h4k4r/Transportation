using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests;

public class CreateDiscountCodeRequest:IValidatableObject
{
    [Required]
    [MinLength(4)]
    public string Code { get; set; } 
    [Required]

    public double Value { get; set; }
    [Required]

    public string Type { get; set; } = null!;
    [Required]

    public Detail Detail { get; set; } 
    [Required]

    public ulong AreaId { get; set; }
    [Required]

    public ushort? UsageLimit { get; set; }
    [Required]
    public DateTime StartAt { get; set; }
    [Required]

    public DateTime? ExpireAt { get; set; }
    [Required]

    public byte Status { get; set; }
    
    public DateTime? CreatedAt { get; set; }=DateTime.Now;
    
    public DateTime? UpdatedAt { get; set; }=DateTime.Now;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        
        if (StartAt > ExpireAt)
            yield return new ValidationResult($"The field {nameof(ExpireAt)} must be greater than {nameof(StartAt)}", new[] { nameof(ExpireAt), nameof(StartAt) });
    }
}

public class Detail
{    [Required]

    public ICollection<Steps> Steps { get; set; }
}

public class Steps
{
    [Required]
    public byte Step { get; set; }
    [Required]

    public uint Max { get; set; }
        [Required]

    public double Value { get; set; }
}