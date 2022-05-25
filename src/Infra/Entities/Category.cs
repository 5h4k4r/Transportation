namespace Infra.Entities
{
    public sealed class Category
    {
        public Category()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            ServiceAreaTypes = new HashSet<ServiceAreaType>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
    }
}
