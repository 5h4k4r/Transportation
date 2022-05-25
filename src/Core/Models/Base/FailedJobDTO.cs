namespace Core.Models.Base;
    public class FailedJobDto
    {
        public ulong Id { get; set; }
        public string Connection { get; set; } = null!;
        public string Queue { get; set; } = null!;
        public string Payload { get; set; } = null!;
        public string Exception { get; set; } = null!;
        public DateTime FailedAt { get; set; }
    
}
