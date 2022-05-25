namespace Infra.Entities
{
    public sealed class Field
    {
        public Field()
        {
            RequestRequirements = new HashSet<RequestRequirement>();
        }

        public ulong Id { get; set; }
        public ulong SegmentId { get; set; }
        public ulong LabelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Label Label { get; set; } = null!;
        public Segment Segment { get; set; } = null!;
        public ICollection<RequestRequirement> RequestRequirements { get; set; }
    }
}
