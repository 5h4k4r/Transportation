using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Log
    {
        public ulong Id { get; set; }
        public short StatusCode { get; set; }
        public string Url { get; set; } = null!;
        public string Inputs { get; set; } = null!;
        public string? Headers { get; set; }
        public string Result { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
