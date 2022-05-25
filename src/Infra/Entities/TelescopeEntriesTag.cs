namespace Infra.Entities
{
    public class TelescopeEntriesTag
    {
        public Guid EntryUuid { get; set; }
        public string Tag { get; set; } = null!;

        public virtual TelescopeEntry EntryUu { get; set; } = null!;
    }
}
