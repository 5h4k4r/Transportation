using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ServiceTranslation
    {
        public ulong Id { get; set; }
        public uint LanguageId { get; set; }
        public ulong ServiceId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
