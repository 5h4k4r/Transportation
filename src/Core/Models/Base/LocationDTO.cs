using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class LocationDTO
    {
        public ulong Id { get; set; }
        public ulong TraceId { get; set; }
        public string? Points { get; set; }
        public string? ModifiedPoints { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
