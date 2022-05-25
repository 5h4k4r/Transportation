namespace Infra.Entities
{
    public sealed class Vehicle
    {
        public Vehicle()
        {
            VehicleDetails = new HashSet<VehicleDetail>();
            VehicleOwners = new HashSet<VehicleOwner>();
            VehicleUsers = new HashSet<VehicleUser>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public ulong? UsageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<VehicleDetail> VehicleDetails { get; set; }
        public ICollection<VehicleOwner> VehicleOwners { get; set; }
        public ICollection<VehicleUser> VehicleUsers { get; set; }
    }
}
