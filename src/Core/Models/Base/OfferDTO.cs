using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class OfferDTO
    {
        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public ulong AreaId { get; set; }
        public ulong? GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string Condition { get; set; } = null!;
        public string Target { get; set; } = null!;
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    
}
