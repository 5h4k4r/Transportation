namespace Infra.Entities
{
    public sealed class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
            CanceledTasks = new HashSet<CanceledTask>();
            Gifts = new HashSet<Gift>();
            Offers = new HashSet<Offer>();
            RoleUsers = new HashSet<RoleUser>();
            UserAreas = new HashSet<UserArea>();
        }

        public byte Id { get; set; }
        /// <summary>
        /// [1 =&gt; [Admin , SuperAdmin], 2 =&gt; [ Client , Servant , Organization ]]
        /// </summary>
        public sbyte Type { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<CanceledTask> CanceledTasks { get; set; }
        public ICollection<Gift> Gifts { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
        public ICollection<UserArea> UserAreas { get; set; }
    }
}
