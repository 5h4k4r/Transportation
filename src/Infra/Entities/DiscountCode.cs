using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class DiscountCode
    {
        public DiscountCode()
        {
            DiscountCodeUsers = new HashSet<DiscountCodeUser>();
        }

        public ulong Id { get; set; }
        public string Code { get; set; } = null!;
        public double Value { get; set; }
        public string Type { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public ulong AreaId { get; set; }
        public ushort? UsageLimit { get; set; }
        public byte Status { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? ExpireAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AreaInfo Area { get; set; } = null!;
        public virtual ICollection<DiscountCodeUser> DiscountCodeUsers { get; set; }
    }
}
