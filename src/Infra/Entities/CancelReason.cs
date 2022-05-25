namespace Infra.Entities
{
    public sealed class CancelReason
    {
        public CancelReason()
        {
            CancelReasonTranslations = new HashSet<CancelReasonTranslation>();
            CanceledTasks = new HashSet<CanceledTask>();
        }

        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<CancelReasonTranslation> CancelReasonTranslations { get; set; }
        public ICollection<CanceledTask> CanceledTasks { get; set; }
    }
}
