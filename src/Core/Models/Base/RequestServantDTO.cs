namespace Core.Models.Base
{
    public class RequestServantDto
    {
        public ulong RequestId { get; set; }
        public string? Online { get; set; }
        public string? Passive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
