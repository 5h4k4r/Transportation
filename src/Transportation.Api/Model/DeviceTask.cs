﻿using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class DeviceTask
    {
        public ulong TaskId { get; set; }
        public ulong DeviceId { get; set; }
        public byte ActiveFromStatus { get; set; }

        public virtual Device Device { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
    }
}
