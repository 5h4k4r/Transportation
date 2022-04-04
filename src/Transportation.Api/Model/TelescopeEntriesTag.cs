using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class TelescopeEntriesTag
    {
        public Guid EntryUuid { get; set; }
        public string Tag { get; set; } = null!;

        public virtual TelescopeEntry EntryUu { get; set; } = null!;
    }
}
