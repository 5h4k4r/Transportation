using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class GenderDTO
    {

        public byte Id { get; set; }
        public string? Key { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
