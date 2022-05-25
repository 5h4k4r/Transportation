namespace Infra.Entities
{
    public class RequestRequirement
    {
        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public ulong FieldId { get; set; }
        public string Value { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Field Field { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
    }
}
