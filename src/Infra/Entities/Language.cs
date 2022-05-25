namespace Infra.Entities
{
    public sealed class Language
    {
        public Language()
        {
            BaseTypeTranslations = new HashSet<BaseTypeTranslation>();
            CategoryTranslations = new HashSet<CategoryTranslation>();
            ClientFiles = new HashSet<ClientFile>();
            DefaultValues = new HashSet<DefaultValue>();
            GenderTranslations = new HashSet<GenderTranslation>();
            PersonTypeTranslations = new HashSet<PersonTypeTranslation>();
            Segments = new HashSet<Segment>();
            ServiceAreaTypeTranslations = new HashSet<ServiceAreaTypeTranslation>();
            ServiceTranslations = new HashSet<ServiceTranslation>();
            ShippingTranslations = new HashSet<ShippingTranslation>();
            SpecificTranslations = new HashSet<SpecificTranslation>();
            UnitTranslations = new HashSet<UnitTranslation>();
            UsageTranslations = new HashSet<UsageTranslation>();
        }

        public uint Id { get; set; }
        public string Title { get; set; } = null!;
        public string Locale { get; set; } = null!;
        public string Direction { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<BaseTypeTranslation> BaseTypeTranslations { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public ICollection<ClientFile> ClientFiles { get; set; }
        public ICollection<DefaultValue> DefaultValues { get; set; }
        public ICollection<GenderTranslation> GenderTranslations { get; set; }
        public ICollection<PersonTypeTranslation> PersonTypeTranslations { get; set; }
        public ICollection<Segment> Segments { get; set; }
        public ICollection<ServiceAreaTypeTranslation> ServiceAreaTypeTranslations { get; set; }
        public ICollection<ServiceTranslation> ServiceTranslations { get; set; }
        public ICollection<ShippingTranslation> ShippingTranslations { get; set; }
        public ICollection<SpecificTranslation> SpecificTranslations { get; set; }
        public ICollection<UnitTranslation> UnitTranslations { get; set; }
        public ICollection<UsageTranslation> UsageTranslations { get; set; }
    }
}
