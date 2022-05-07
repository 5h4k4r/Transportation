using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Language
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

        public virtual ICollection<BaseTypeTranslation> BaseTypeTranslations { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<ClientFile> ClientFiles { get; set; }
        public virtual ICollection<DefaultValue> DefaultValues { get; set; }
        public virtual ICollection<GenderTranslation> GenderTranslations { get; set; }
        public virtual ICollection<PersonTypeTranslation> PersonTypeTranslations { get; set; }
        public virtual ICollection<Segment> Segments { get; set; }
        public virtual ICollection<ServiceAreaTypeTranslation> ServiceAreaTypeTranslations { get; set; }
        public virtual ICollection<ServiceTranslation> ServiceTranslations { get; set; }
        public virtual ICollection<ShippingTranslation> ShippingTranslations { get; set; }
        public virtual ICollection<SpecificTranslation> SpecificTranslations { get; set; }
        public virtual ICollection<UnitTranslation> UnitTranslations { get; set; }
        public virtual ICollection<UsageTranslation> UsageTranslations { get; set; }
    }
}
