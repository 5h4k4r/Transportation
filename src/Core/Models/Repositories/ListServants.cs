namespace Core.Models.Repositories;

public class ListServants
{
    // public ServantDto servant { get; set; }
    public int Id { get; set; }
    public ulong UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NationalId { get; set; } = null!;
    public string? Certificate { get; set; }
    public string? BankId { get; set; }
    public uint AreaId { get; set; }
    public byte? GenderId { get; set; }
    public string Address { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public string Mobile { get; set; }

    public string Status { get; set; } = null!;
}