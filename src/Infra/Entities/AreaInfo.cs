namespace Infra.Entities
{
    public sealed class AreaInfo
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

        public ICollection<AreaDepartment> AreaDepartments { get; set; }
        public ICollection<DailyStatistic> DailyStatistics { get; set; }
        public ICollection<DiscountCode> DiscountCodes { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
        public ICollection<SupportNumber> SupportNumbers { get; set; }
        public ICollection<TaskHourlyStatistic> TaskHourlyStatistics { get; set; }
        public ICollection<UserArea> UserAreas { get; set; }
    }
}
