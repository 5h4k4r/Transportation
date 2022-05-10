using System;
using System.Collections.Generic;

namespace Core.Models;

    public partial class ActionUsageDTO
    {
        public ulong ActionId { get; set; }
        public ulong UsageId { get; set; }
        public string Value { get; set; } = null!;

}
