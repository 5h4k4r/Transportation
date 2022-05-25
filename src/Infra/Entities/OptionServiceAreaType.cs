namespace Infra.Entities
{
    public class OptionServiceAreaType
    {
        public ulong Id { get; set; }
        public ulong OptionId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Option Option { get; set; } = null!;
        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
    }
}
