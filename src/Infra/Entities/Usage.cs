using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Usage
    {
        public Usage()
        {
            ActionUsages = new HashSet<ActionUsage>();
            ServiceAreaTypes = new HashSet<ServiceAreaType>();
            UsageTranslations = new HashSet<UsageTranslation>();
        }

        public ulong Id { get; set; }
        public string? StaticKey { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<ActionUsage> ActionUsages { get; set; }
        public virtual ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
        public virtual ICollection<UsageTranslation> UsageTranslations { get; set; }
    }
}
