using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ServiceAreaTypeTranslation
    {
        public ulong Id { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public uint LanguageId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
    }
}
