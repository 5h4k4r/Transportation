namespace Infra.Entities
{
    public class SupportNumber
    {
        public ulong Id { get; set; }
        public ulong AreaId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual AreaInfo Area { get; set; } = null!;
    }
}
