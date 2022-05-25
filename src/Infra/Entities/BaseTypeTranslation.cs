namespace Infra.Entities
{
    public class BaseTypeTranslation
    {
        public ulong Id { get; set; }
        public ulong BaseTypeId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual BaseType BaseType { get; set; } = null!;
        public virtual Language Language { get; set; } = null!;
    }
}
