using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class UserTokenDTO
    {
        public ulong UserId { get; set; }
        public string? Environment { get; set; }
        public string Shadow { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
