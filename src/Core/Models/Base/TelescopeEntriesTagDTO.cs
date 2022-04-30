using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class TelescopeEntriesTagDTO
    {
        public Guid EntryUuid { get; set; }
        public string Tag { get; set; } = null!;

    }
}
