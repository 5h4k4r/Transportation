namespace Core.Models.Base
{
    public class SegmentDto
    {
       
        public ulong Id { get; set; }
        public uint RequirementId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
