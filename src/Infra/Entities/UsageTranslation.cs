namespace Infra.Entities
{
    public class UsageTranslation
    {
        public ulong Id { get; set; }
        public ulong UsageId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual Usage Usage { get; set; } = null!;
    }
}
