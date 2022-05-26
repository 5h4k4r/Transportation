namespace Infra.Entities
{
    public class Score
    {
        public ulong Id { get; set; }
        public ulong TaskId { get; set; }
        public ulong UserId { get; set; }
        public double Rate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Task Task { get; set; } = null!;
    }
}
