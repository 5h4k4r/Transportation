using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class CancelReason
    {
        public CancelReason()
        {
            CancelReasonTranslations = new HashSet<CancelReasonTranslation>();
            CanceledTasks = new HashSet<CanceledTask>();
        }

        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<CancelReasonTranslation> CancelReasonTranslations { get; set; }
        public virtual ICollection<CanceledTask> CanceledTasks { get; set; }
    }
}
