namespace Infra.Entities
{
    public sealed class Group
    {
        public Group()
        {
            GroupUsers = new HashSet<GroupUser>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
