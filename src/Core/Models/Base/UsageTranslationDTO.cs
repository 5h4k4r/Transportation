using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class UsageTranslationDTO
    {
        public ulong Id { get; set; }
        public ulong UsageId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
