using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ShippingTranslation
    {
        public ulong Id { get; set; }
        public ulong ShippingId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual Shipping Shipping { get; set; } = null!;
    }
}
