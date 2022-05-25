namespace Core.Models.Base
{
    public class ServiceAreaTypeTranslationDto
    {
        public ulong Id { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public uint LanguageId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
