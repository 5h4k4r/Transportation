namespace Core.Models.Base
{
    public class VehicleOwnerDto
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong VehicleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual UserDto User { get; set; } = null!;
    }
}
