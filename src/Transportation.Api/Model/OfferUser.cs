﻿using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class OfferUser
    {
        public ulong OfferId { get; set; }
        public ulong UserId { get; set; }
        public float Progress { get; set; }
        public ushort TasksDone { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
