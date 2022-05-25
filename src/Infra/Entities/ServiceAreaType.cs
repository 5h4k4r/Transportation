namespace Infra.Entities
{
    public sealed class ServiceAreaType
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

        public AreaInfo Area { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public Type? Type { get; set; }
        public Usage? Usage { get; set; }
        public ICollection<AttributeServiceAreaType> AttributeServiceAreaTypes { get; set; }
        public ICollection<Commission> Commissions { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<NoServantRequest> NoServantRequests { get; set; }
        public ICollection<OptionServiceAreaType> OptionServiceAreaTypes { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Requirement> Requirements { get; set; }
        public ICollection<ServiceAreaTypeTranslation> ServiceAreaTypeTranslations { get; set; }
    }
}
