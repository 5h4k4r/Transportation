using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Referral
    {
        public ulong Id { get; set; }
        public ulong InviterId { get; set; }
        public byte InviterRoleId { get; set; }
        public ulong InvitedId { get; set; }
        public ulong InvitedRoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
