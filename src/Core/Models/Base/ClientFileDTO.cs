using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class ClientFileDTO
    {
        public ulong Id { get; set; }
        public ulong FileId { get; set; }
        public uint LanguageId { get; set; }
        public string Platform { get; set; } = null!;
        public string? Version { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
