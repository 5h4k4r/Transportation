using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class DiscountCodeServiceAreaType
    {
        public int Id { get; set; }
        public ulong DiscountCodeId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }

        public virtual DiscountCode DiscountCode { get; set; } = null!;
        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
    }
}
