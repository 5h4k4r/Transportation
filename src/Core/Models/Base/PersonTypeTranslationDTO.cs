using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class PersonTypeTranslationDTO
    {
        public ulong Id { get; set; }
        public ulong PersonTypeId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
