namespace Infra.Entities
{
    public sealed class Segment
    {
        public Segment()
        {
            Fields = new HashSet<Field>();
        }

        public ulong Id { get; set; }
        public uint RequirementId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Language Language { get; set; } = null!;
        public Requirement Requirement { get; set; } = null!;
        public ICollection<Field> Fields { get; set; }
    }
}
