namespace Core.Models.Base
{
    public class VehicleUserDto
    {
        public ulong Id { get; set; }
        public ulong VehicleId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
