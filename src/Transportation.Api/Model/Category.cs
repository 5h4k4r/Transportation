using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Category
    {
        public Category()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            ServiceAreaTypes = new HashSet<ServiceAreaType>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
    }
}
