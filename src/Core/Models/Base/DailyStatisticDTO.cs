namespace Core.Models.Base;
    public class DailyStatisticDto
    {
        public ulong Id { get; set; }
        public ulong DayId { get; set; }
        public ulong ServiceId { get; set; }
        public ulong AreaId { get; set; }
        public uint OnlineServants { get; set; }
        /// <summary>
        /// hour base
        /// </summary>
        public double OnlineHours { get; set; }
        public uint Requests { get; set; }
        public uint NoServantRequests { get; set; }
        public uint NoAcceptRequests { get; set; }
        public uint SuccessTasks { get; set; }
        public uint CanceledTasks { get; set; }
        /// <summary>
        /// hour base
        /// </summary>
        public double DurationOnTask { get; set; }
        /// <summary>
        /// km base
        /// </summary>
        public double DistanceOnTask { get; set; }

}
