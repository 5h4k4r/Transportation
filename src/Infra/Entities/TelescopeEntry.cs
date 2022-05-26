namespace Infra.Entities
{
    public class TelescopeEntry
    {
        public ulong Sequence { get; set; }
        public Guid Uuid { get; set; }
        public Guid BatchId { get; set; }
        public string? FamilyHash { get; set; }
        public bool? ShouldDisplayOnIndex { get; set; }
        public string Type { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
