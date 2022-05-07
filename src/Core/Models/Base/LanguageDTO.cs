using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class LanguageDTO
    {

        public uint Id { get; set; }
        public string Title { get; set; } = null!;
        public string Locale { get; set; } = null!;
        public string Direction { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
