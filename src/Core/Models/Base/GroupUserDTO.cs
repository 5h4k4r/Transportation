using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class GroupUserDTO
    {
        public ulong Id { get; set; }
        public ulong GroupId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
