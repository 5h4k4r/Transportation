namespace Core.Models.Base
{
    public class ScoreDto
    {
        public ulong Id { get; set; }
        public ulong TaskId { get; set; }
        public ulong UserId { get; set; }
        public double Rate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
