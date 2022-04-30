using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class DeviceTaskDTO
    {
        public ulong TaskId { get; set; }
        public ulong DeviceId { get; set; }
        public byte ActiveFromStatus { get; set; }

}
