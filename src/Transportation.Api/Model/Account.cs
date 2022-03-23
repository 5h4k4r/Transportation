using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Account
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public ulong ServiceId { get; set; }
        public string Number { get; set; } = null!;
        public long? Balance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
