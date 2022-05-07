using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class GroupUser
    {
        public ulong Id { get; set; }
        public ulong GroupId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
