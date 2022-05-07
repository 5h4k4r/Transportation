using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class PersonTypeTranslation
    {
        public ulong Id { get; set; }
        public ulong PersonTypeId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual PersonType PersonType { get; set; } = null!;
    }
}
