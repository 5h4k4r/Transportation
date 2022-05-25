namespace Infra.Entities
{
    public sealed class Shipping
    {
        public Shipping()
        {
            ShippingTranslations = new HashSet<ShippingTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<ShippingTranslation> ShippingTranslations { get; set; }
    }
}
