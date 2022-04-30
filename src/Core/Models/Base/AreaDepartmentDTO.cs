﻿using System;
using System.Collections.Generic;

namespace Core.Models;

    public partial class AreaDepartmentDTO
    {
        public ulong Id { get; set; }
        public ulong? AreaId { get; set; }
        public uint? DepartmentId { get; set; }
        public ulong? RoleUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
