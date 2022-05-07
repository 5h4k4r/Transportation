using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class ActiveRole
    {
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
