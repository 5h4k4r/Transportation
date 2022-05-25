namespace Core.Models.Base;
    public class LocationDto
    {
        public ulong Id { get; set; }
        public ulong TraceId { get; set; }
        public string? Points { get; set; }
        public string? ModifiedPoints { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
