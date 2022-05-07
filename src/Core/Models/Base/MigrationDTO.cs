using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class MigrationDTO
    {
        public uint Id { get; set; }
        public string Migration1 { get; set; } = null!;
        public int Batch { get; set; }
    }
}
