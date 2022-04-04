﻿using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class DiscountCodeServiceAreaType
    {
        public ulong DiscountCodeId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }

        public virtual DiscountCode DiscountCode { get; set; } = null!;
        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
    }
}
