using System;
using System.Collections.Generic;

namespace Core.Models;

    public partial class ActionDTO
    {

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public string Format { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
