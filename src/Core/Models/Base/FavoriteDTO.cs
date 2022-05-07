using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class FavoriteDTO
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Title { get; set; } = null!;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
