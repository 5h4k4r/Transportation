using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class City
    {
        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public uint CountryId { get; set; }
        public string TimeZone { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Country Country { get; set; } = null!;
    }
}
