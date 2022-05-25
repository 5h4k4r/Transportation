namespace Infra.Entities
{
    public sealed class Action
    {
        public Action()
        {
            ActionUsages = new HashSet<ActionUsage>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public string Format { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<ActionUsage> ActionUsages { get; set; }
    }
}
