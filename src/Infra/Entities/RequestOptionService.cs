using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class RequestOptionService
    {
        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public ulong ServiceId { get; set; }
        public ulong OptionId { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Option Option { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
