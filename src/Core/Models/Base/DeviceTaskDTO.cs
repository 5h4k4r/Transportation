namespace Core.Models.Base;
    public class DeviceTaskDto
    {
        public ulong TaskId { get; set; }
        public ulong DeviceId { get; set; }
        public byte ActiveFromStatus { get; set; }

}
