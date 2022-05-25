namespace Infra.Entities
{
    public sealed class Attribute
    {
        public Attribute()
        {
            AttributeServiceAreaTypes = new HashSet<AttributeServiceAreaType>();
        }

        public ulong Id { get; set; }
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<AttributeServiceAreaType> AttributeServiceAreaTypes { get; set; }
    }
}
