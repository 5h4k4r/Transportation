using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Department
    {
        public Department()
        {
            AreaDepartments = new HashSet<AreaDepartment>();
            DepartmentRoleUsers = new HashSet<DepartmentRoleUser>();
        }

        public uint Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<AreaDepartment> AreaDepartments { get; set; }
        public virtual ICollection<DepartmentRoleUser> DepartmentRoleUsers { get; set; }
    }
}
