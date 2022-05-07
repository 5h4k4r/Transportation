using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Role
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

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CanceledTask> CanceledTasks { get; set; }
        public virtual ICollection<Gift> Gifts { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        public virtual ICollection<UserArea> UserAreas { get; set; }
    }
}
