using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class OfferServiceAreaType
    {
        public ulong OfferId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }

        public virtual Offer Offer { get; set; } = null!;
        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
    }
}
