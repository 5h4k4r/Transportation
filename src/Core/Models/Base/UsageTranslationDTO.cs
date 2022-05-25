namespace Core.Models.Base;
    public class UsageTranslationDto
    {
        public ulong Id { get; set; }
        public ulong UsageId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
