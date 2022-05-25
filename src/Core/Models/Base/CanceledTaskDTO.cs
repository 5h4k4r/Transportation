﻿namespace Core.Models.Base;

    public class CanceledTaskDto
    {
        public ulong Id { get; set; }
        public ulong TaskId { get; set; }
        public ulong? CancelReasonId { get; set; }
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
