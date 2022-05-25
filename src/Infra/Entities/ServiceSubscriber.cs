namespace Infra.Entities
{
    public class ServiceSubscriber
    {
        public ulong Id { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public bool? IsSubscribed { get; set; }
        public string? WorkTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
