namespace Core.Models.Base
{
    public class TaskDto
    {

        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public ulong ServantId { get; set; }
        public int Price { get; set; }
        public int Tip { get; set; }
        public sbyte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
