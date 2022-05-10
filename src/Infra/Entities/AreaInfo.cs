using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class AreaInfo
    {
        public AreaInfo()
        {
            AreaDepartments = new HashSet<AreaDepartment>();
            DailyStatistics = new HashSet<DailyStatistic>();
            DiscountCodes = new HashSet<DiscountCode>();
            Offers = new HashSet<Offer>();
            ServiceAreaTypes = new HashSet<ServiceAreaType>();
            SupportNumbers = new HashSet<SupportNumber>();
            TaskHourlyStatistics = new HashSet<TaskHourlyStatistic>();
            UserAreas = new HashSet<UserArea>();
        }

        public ulong Id { get; set; }
        public string AreaId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Currency { get; set; }
        public string? Country { get; set; }
        public string? Timezone { get; set; }
        public string? Center { get; set; }
        public string? Bound { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<AreaDepartment> AreaDepartments { get; set; }
        public virtual ICollection<DailyStatistic> DailyStatistics { get; set; }
        public virtual ICollection<DiscountCode> DiscountCodes { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
        public virtual ICollection<SupportNumber> SupportNumbers { get; set; }
        public virtual ICollection<TaskHourlyStatistic> TaskHourlyStatistics { get; set; }
        public virtual ICollection<UserArea> UserAreas { get; set; }
    }
}
