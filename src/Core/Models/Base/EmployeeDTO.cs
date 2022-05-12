using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class EmployeeDTO
    {
        public ulong UserId { get; set; }
        public ulong? AreaId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public uint? LanguageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public AreaInfoDTO? AreaInfo { get; set; }

    }
}
