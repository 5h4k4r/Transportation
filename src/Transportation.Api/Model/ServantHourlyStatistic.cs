using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ServantHourlyStatistic
    {
        public ulong Id { get; set; }
        public ulong DayId { get; set; }
        public ulong ServiceId { get; set; }
        public ulong AreaId { get; set; }
        public byte Hour { get; set; }
        public uint OnlineServants { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
