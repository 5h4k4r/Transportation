using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Action
    {
        public Action()
        {
            ActionUsages = new HashSet<ActionUsage>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public string Format { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ActionUsage> ActionUsages { get; set; }
    }
}
