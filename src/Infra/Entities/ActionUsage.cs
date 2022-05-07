using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class ActionUsage
    {
        public ulong ActionId { get; set; }
        public ulong UsageId { get; set; }
        public string Value { get; set; } = null!;

        public virtual Action Action { get; set; } = null!;
        public virtual Usage Usage { get; set; } = null!;
    }
}
