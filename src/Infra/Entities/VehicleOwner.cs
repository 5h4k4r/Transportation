namespace Infra.Entities
{
    public class VehicleOwner
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong VehicleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
