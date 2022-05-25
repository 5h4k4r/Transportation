namespace Infra.Entities
{
    public sealed class BaseType
    {
        public BaseType()
        {
            BaseTypeTranslations = new HashSet<BaseTypeTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<BaseTypeTranslation> BaseTypeTranslations { get; set; }
    }
}
