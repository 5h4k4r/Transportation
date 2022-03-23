using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Shipping
    {
        public Shipping()
        {
            ShippingTranslations = new HashSet<ShippingTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<ShippingTranslation> ShippingTranslations { get; set; }
    }
}
