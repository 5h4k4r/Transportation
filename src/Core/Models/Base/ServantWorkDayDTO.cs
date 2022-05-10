using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ServantWorkDayDTO
    {

        public ulong Id { get; set; }
        public DateOnly Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

 
    }
}
