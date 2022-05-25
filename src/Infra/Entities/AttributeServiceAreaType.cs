namespace Infra.Entities
{
    public class AttributeServiceAreaType
    {
        public ulong Id { get; set; }
        public ulong AttributeId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public string? Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Attribute Attribute { get; set; } = null!;
        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
    }
}
