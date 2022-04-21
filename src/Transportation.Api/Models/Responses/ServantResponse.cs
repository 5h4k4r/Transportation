namespace Tranportation.Api.Responses;
using Transportation.Api.Model;
public class ServantResponse
{
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

    public ServantResponse(Servant model)
    {

        Id = model.Id;
        UserId = model.UserId;
        FirstName = model.FirstName;
        LastName = model.LastName;
        NationalId = model.NationalId;
        Certificate = model.Certificate;
        BankId = model.BankId;
        AreaId = model.AreaId;
        GenderId = model.GenderId;
        Address = model.Address;
        CreatedAt = model.CreatedAt;
        UpdatedAt = model.UpdatedAt;

    }

}