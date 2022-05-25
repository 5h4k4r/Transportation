namespace Infra.Entities
{
    public sealed class Requirement
    {
        public Requirement()
        {
            Segments = new HashSet<Segment>();
        }

        public uint Id { get; set; }
        public string Type { get; set; } = null!;
        public string ShowIn { get; set; } = null!;
        public ulong ServiceAreaTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ServiceAreaType ServiceAreaType { get; set; } = null!;
        public ICollection<Segment> Segments { get; set; }
    }
}
