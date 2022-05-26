namespace Infra.Entities
{
    public class CategoryTranslation
    {
        public ulong Id { get; set; }
        public ulong CategoryId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Language Language { get; set; } = null!;
    }
}
