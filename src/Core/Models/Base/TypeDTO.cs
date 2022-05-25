namespace Core.Models.Base;
    public class TypeDto
    {

        public ulong Id { get; set; }
        public ulong? ShippingId { get; set; }
        public ulong? BaseTypeId { get; set; }
        public ulong? PersonTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
