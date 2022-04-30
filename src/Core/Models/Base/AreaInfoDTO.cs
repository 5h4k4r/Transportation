using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class AreaInfoDTO
    {

        public ulong Id { get; set; }
        public string AreaId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Currency { get; set; }
        public string? Country { get; set; }
        public string? Timezone { get; set; }
        public string? Center { get; set; }
        public string? Bound { get; set; }
        public DateTime? CreatedAt { get; set; }
 
}
