namespace Core.Models.Base;
    public class DeviceDto
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Platform { get; set; } = null!;
        public string DeviceId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
