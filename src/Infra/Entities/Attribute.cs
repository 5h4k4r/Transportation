using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Attribute
    {
        public Attribute()
        {
            AttributeServiceAreaTypes = new HashSet<AttributeServiceAreaType>();
        }

        public ulong Id { get; set; }
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<AttributeServiceAreaType> AttributeServiceAreaTypes { get; set; }
    }
}
