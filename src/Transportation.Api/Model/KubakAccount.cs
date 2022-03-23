using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class KubakAccount
    {
        public ulong Id { get; set; }
        public ulong? AreaId { get; set; }
        public string? Title { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
