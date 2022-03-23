using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class BaseType
    {
        public BaseType()
        {
            BaseTypeTranslations = new HashSet<BaseTypeTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<BaseTypeTranslation> BaseTypeTranslations { get; set; }
    }
}
