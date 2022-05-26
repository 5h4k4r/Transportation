namespace Infra.Entities
{
    public class SpecificTranslation
    {
        public ulong Id { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Language Language { get; set; } = null!;
    }
}
