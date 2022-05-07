using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class ServiceAreaType
    {
        public ServiceAreaType()
        {
            AttributeServiceAreaTypes = new HashSet<AttributeServiceAreaType>();
            Commissions = new HashSet<Commission>();
            Discounts = new HashSet<Discount>();
            NoServantRequests = new HashSet<NoServantRequest>();
            OptionServiceAreaTypes = new HashSet<OptionServiceAreaType>();
            Requests = new HashSet<Request>();
            Requirements = new HashSet<Requirement>();
            ServiceAreaTypeTranslations = new HashSet<ServiceAreaTypeTranslation>();
        }

        public ulong Id { get; set; }
        public ulong ServiceId { get; set; }
        public string AreaId { get; set; } = null!;
        public ulong CategoryId { get; set; }
        public ulong? TypeId { get; set; }
        public ulong? UsageId { get; set; }
        public string Params { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AreaInfo Area { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual Type? Type { get; set; }
        public virtual Usage? Usage { get; set; }
        public virtual ICollection<AttributeServiceAreaType> AttributeServiceAreaTypes { get; set; }
        public virtual ICollection<Commission> Commissions { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<NoServantRequest> NoServantRequests { get; set; }
        public virtual ICollection<OptionServiceAreaType> OptionServiceAreaTypes { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
        public virtual ICollection<ServiceAreaTypeTranslation> ServiceAreaTypeTranslations { get; set; }
    }
}
