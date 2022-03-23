using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Department
    {
        public Department()
        {
            AreaDepartments = new HashSet<AreaDepartment>();
        }

        public uint Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<AreaDepartment> AreaDepartments { get; set; }
    }
}
