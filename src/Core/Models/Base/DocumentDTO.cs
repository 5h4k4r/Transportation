using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class DocumentDTO
    {
        public ulong Id { get; set; }
        public string Path { get; set; } = null!;
        public string Type { get; set; } = null!;
        public bool IsVerified { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    
}
