namespace Core.Models.Base;
    public class DeadLineDto
    {
        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
        public TimeOnly GoingTime { get; set; }
  
}
