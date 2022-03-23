using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Gift
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public int Balance { get; set; }
        public ulong TransferredToPayment { get; set; }
        public string Type { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
