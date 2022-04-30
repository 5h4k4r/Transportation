using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ShippingTranslationDTO
    {
        public ulong Id { get; set; }
        public ulong ShippingId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
