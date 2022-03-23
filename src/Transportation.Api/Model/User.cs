using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            ActiveRoles = new HashSet<ActiveRole>();
            CanceledTasks = new HashSet<CanceledTask>();
            Devices = new HashSet<Device>();
            DiscountCodeUsers = new HashSet<DiscountCodeUser>();
            Favorites = new HashSet<Favorite>();
            Gifts = new HashSet<Gift>();
            GroupUsers = new HashSet<GroupUser>();
            Members = new HashSet<Member>();
            NoServantRequests = new HashSet<NoServantRequest>();
            RoleUsers = new HashSet<RoleUser>();
            UserAreas = new HashSet<UserArea>();
            UserTokens = new HashSet<UserToken>();
            VehicleOwners = new HashSet<VehicleOwner>();
            VehicleUsers = new HashSet<VehicleUser>();
        }

        public ulong Id { get; set; }
        public string Mobile { get; set; } = null!;
        public string? Name { get; set; }
        public string? AuthId { get; set; }
        public byte? GenderId { get; set; }
        public uint? LanguageId { get; set; }
        public DateOnly? BirthDate { get; set; }
        public ulong? AreaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Gender? Gender { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual Servant Servant { get; set; } = null!;
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<ActiveRole> ActiveRoles { get; set; }
        public virtual ICollection<CanceledTask> CanceledTasks { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<DiscountCodeUser> DiscountCodeUsers { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Gift> Gifts { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<NoServantRequest> NoServantRequests { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        public virtual ICollection<UserArea> UserAreas { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<VehicleOwner> VehicleOwners { get; set; }
        public virtual ICollection<VehicleUser> VehicleUsers { get; set; }
    }
}
