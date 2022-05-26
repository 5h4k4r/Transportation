namespace Infra.Entities
{
    public sealed class Usage
    {
        public Usage()
        {
            ActionUsages = new HashSet<ActionUsage>();
            ServiceAreaTypes = new HashSet<ServiceAreaType>();
            UsageTranslations = new HashSet<UsageTranslation>();
        }

        public ulong Id { get; set; }
        public string? StaticKey { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<ActionUsage> ActionUsages { get; set; }
        public ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
        public ICollection<UsageTranslation> UsageTranslations { get; set; }
    }
}
