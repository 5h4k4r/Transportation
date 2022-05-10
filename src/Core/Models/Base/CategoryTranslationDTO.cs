using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class CategoryTranslationDTO
    {
        public ulong Id { get; set; }
        public ulong CategoryId { get; set; }
        public uint LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
