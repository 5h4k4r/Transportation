﻿using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class RoleUserDTO
    {

        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
