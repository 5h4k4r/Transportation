using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class DiscountCodeUser
    {
        public ulong Id { get; set; }
        public ulong DiscountCodeId { get; set; }
        public ulong UserId { get; set; }
        public ushort Amount { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public bool Used { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual DiscountCode DiscountCode { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
