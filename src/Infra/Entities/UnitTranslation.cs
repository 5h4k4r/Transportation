using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class UnitTranslation
    {
        public ulong Id { get; set; }
        public ulong UnitId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual Unit Unit { get; set; } = null!;
    }
}
