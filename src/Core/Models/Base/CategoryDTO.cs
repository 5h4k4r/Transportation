namespace Core.Models.Base;

public class CategoryDto
{
    public ulong Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<CategoryTranslationDto?>? CategoryTranslations { get; set; }
}