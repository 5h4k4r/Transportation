namespace Infra.Entities
{
    public class KubakAccount
    {
        public ulong Id { get; set; }
        public ulong? CityId { get; set; }
        public string? Title { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
