using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class RoleUser
    {
        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
