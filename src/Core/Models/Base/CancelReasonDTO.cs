namespace Core.Models.Base;
    public class CancelReasonDto
    {
        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
