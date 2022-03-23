using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ActionUsage
    {
        public int Id { get; set; }
        public ulong ActionId { get; set; }
        public ulong UsageId { get; set; }
        public string Value { get; set; } = null!;

        public virtual Action Action { get; set; } = null!;
        public virtual Usage Usage { get; set; } = null!;
    }
}
