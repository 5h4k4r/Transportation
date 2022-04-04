using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Task
    {
        public Task()
        {
            CanceledTasks = new HashSet<CanceledTask>();
            MemberPaymentTypes = new HashSet<MemberPaymentType>();
            Scores = new HashSet<Score>();
            TaskFactors = new HashSet<TaskFactor>();
        }

        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public ulong ServantId { get; set; }
        public int Price { get; set; }
        public int Tip { get; set; }
        public sbyte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Request Request { get; set; } = null!;
        public virtual Servant Servant { get; set; } = null!;
        public virtual ICollection<CanceledTask> CanceledTasks { get; set; }
        public virtual ICollection<MemberPaymentType> MemberPaymentTypes { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<TaskFactor> TaskFactors { get; set; }
    }
}
