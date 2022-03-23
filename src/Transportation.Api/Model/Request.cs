using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Request
    {
        public Request()
        {
            DeadLines = new HashSet<DeadLine>();
            RequestOptionServices = new HashSet<RequestOptionService>();
            RequestRequirements = new HashSet<RequestRequirement>();
            Tasks = new HashSet<Task>();
        }

        public ulong Id { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public DateTime? ReserveTime { get; set; }
        public int KubakPrice { get; set; }
        public int UserPrice { get; set; }
        public string? Discount { get; set; }
        public string? Type { get; set; }
        public sbyte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
        public virtual RequestServant RequestServant { get; set; } = null!;
        public virtual ICollection<DeadLine> DeadLines { get; set; }
        public virtual ICollection<RequestOptionService> RequestOptionServices { get; set; }
        public virtual ICollection<RequestRequirement> RequestRequirements { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
