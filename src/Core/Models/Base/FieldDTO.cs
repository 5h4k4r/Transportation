using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class FieldDTO
    {
     
        public ulong Id { get; set; }
        public ulong SegmentId { get; set; }
        public ulong LabelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    
}
