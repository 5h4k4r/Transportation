namespace Core.Models.Base;
    public class TaskMessageDto
    {
        public ulong TaskId { get; set; }
        public string? ChatId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

