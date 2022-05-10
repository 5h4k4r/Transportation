using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class OptionServiceAreaTypeDTO
    {
        public ulong Id { get; set; }
        public ulong OptionId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
