using System;
using System.Collections.Generic;

namespace Core.Models;

    public partial class AccountDTO
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public ulong ServiceId { get; set; }
        public string Number { get; set; } = null!;
        public long? Balance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
