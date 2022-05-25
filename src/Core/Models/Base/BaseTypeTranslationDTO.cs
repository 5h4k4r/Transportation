namespace Core.Models.Base;
    public class BaseTypeTranslationDto
    {
        public ulong Id { get; set; }
        public ulong BaseTypeId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
