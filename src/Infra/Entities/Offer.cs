namespace Infra.Entities
{
    public class Offer
    {
        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public ulong AreaId { get; set; }
        public ulong? GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string Condition { get; set; } = null!;
        public string Target { get; set; } = null!;
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AreaInfo Area { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
