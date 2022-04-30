using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            Labels = new HashSet<Label>();
            UnitTranslations = new HashSet<UnitTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Label> Labels { get; set; }
        public virtual ICollection<UnitTranslation> UnitTranslations { get; set; }
    }
}
