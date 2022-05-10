using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class GenderTranslation
    {
        public ulong Id { get; set; }
        public byte GenderId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Gender Gender { get; set; } = null!;
        public virtual Language Language { get; set; } = null!;
    }
}
