namespace Infra.Entities
{
    public class RequestServant
    {
        public ulong RequestId { get; set; }
        public string? Online { get; set; }
        public string? Passive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Request Request { get; set; } = null!;
    }
}
