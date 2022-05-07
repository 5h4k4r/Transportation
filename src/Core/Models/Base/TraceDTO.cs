using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class TraceDTO
    {

        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string UserType { get; set; } = null!;
        public sbyte Status { get; set; }
        public string ActionPoint { get; set; } = null!;
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
