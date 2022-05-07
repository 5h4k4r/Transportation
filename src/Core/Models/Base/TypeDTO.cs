using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class TypeDTO
    {

        public ulong Id { get; set; }
        public ulong? ShippingId { get; set; }
        public ulong? BaseTypeId { get; set; }
        public ulong? PersonTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
