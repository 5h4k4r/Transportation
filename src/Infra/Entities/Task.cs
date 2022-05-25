namespace Infra.Entities
{
    public sealed class Task
    {
        public Task()
        {
            CanceledTasks = new HashSet<CanceledTask>();
            MemberPaymentTypes = new HashSet<MemberPaymentType>();
            Scores = new HashSet<Score>();
            TaskFactors = new HashSet<TaskFactor>();
        }

        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public ulong ServantId { get; set; }
        public int Price { get; set; }
        public int Tip { get; set; }
        public sbyte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Request Request { get; set; } = null!;
        public Servant Servant { get; set; } = null!;
        public ICollection<CanceledTask> CanceledTasks { get; set; }
        public ICollection<MemberPaymentType> MemberPaymentTypes { get; set; }
        public ICollection<Score> Scores { get; set; }
        public ICollection<TaskFactor> TaskFactors { get; set; }
    }
}
