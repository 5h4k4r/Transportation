using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Employee
    {
        public Employee()
        {
            ActivityLogs = new HashSet<ActivityLog>();
        }

        public int Id { get; set; }
        public ulong UserId { get; set; }
        public ulong? AreaId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public uint? LanguageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
    }
}
