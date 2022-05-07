using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class AttributeServiceAreaTypeDTO
    {
        public ulong Id { get; set; }
        public ulong AttributeId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public string? Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
