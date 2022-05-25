﻿namespace Core.Models.Base
{
    public class TaskFactorDto
    {
        public ulong Id { get; set; }
        public ulong TaskId { get; set; }
        public string Data { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Task Task { get; set; } = null!;
    }
}
