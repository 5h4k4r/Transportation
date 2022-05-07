using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class RequestOptionServiceDTO
    {
        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public ulong ServiceId { get; set; }
        public ulong OptionId { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
