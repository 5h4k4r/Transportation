using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class TelescopeEntriesTag
    {
        public Guid EntryUuid { get; set; }
        public string Tag { get; set; } = null!;

        public virtual TelescopeEntry EntryUu { get; set; } = null!;
    }
}
