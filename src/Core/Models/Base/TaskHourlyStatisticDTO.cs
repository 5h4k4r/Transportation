namespace Core.Models.Base
{
    public class TaskHourlyStatisticDto
    {
        public ulong Id { get; set; }
        public ulong DayId { get; set; }
        public ulong ServiceId { get; set; }
        public ulong AreaId { get; set; }
        public byte Hour { get; set; }
        public uint Requests { get; set; }
        public uint NoServantRequests { get; set; }
        public uint NoAcceptRequests { get; set; }
        public uint SuccessTasks { get; set; }
        public uint CanceledTasks { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
