namespace Infra.Entities;

public sealed class User
{
    public User()
    {
        Accounts = new HashSet<Account>();
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

    public Gender? Gender { get; set; }
    public Language? Language { get; set; }
    public Servant Servant { get; set; } = null!;
    public ICollection<Account> Accounts { get; set; }
    public ICollection<CanceledTask> CanceledTasks { get; set; }
    public ICollection<Device> Devices { get; set; }
    public ICollection<DiscountCodeUser> DiscountCodeUsers { get; set; }
    public ICollection<Favorite> Favorites { get; set; }
    public ICollection<Gift> Gifts { get; set; }
    public ICollection<GroupUser> GroupUsers { get; set; }
    public ICollection<Member> Members { get; set; }
    public ICollection<NoServantRequest> NoServantRequests { get; set; }
    public ICollection<RoleUser> RoleUsers { get; set; }
    public ICollection<UserArea> UserAreas { get; set; }
    public ICollection<VehicleOwner> VehicleOwners { get; set; }
    public ICollection<VehicleUser> VehicleUsers { get; set; }
}