using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class LabelDTO
    {

        public ulong Id { get; set; }
        public ulong? UnitId { get; set; }
        public string? StaticKey { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
