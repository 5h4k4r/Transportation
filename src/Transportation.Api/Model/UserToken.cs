using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class UserToken
    {
        public ulong UserId { get; set; }
        public string? Environment { get; set; }
        public string Shadow { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
