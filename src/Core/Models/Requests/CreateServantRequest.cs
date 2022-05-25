namespace Core.Models.Requests;


public class CreateServantRequest
{

    public ulong UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NationalId { get; set; } = null!;
    public string? Certificate { get; set; }
    public string? CertificateNum { get; set; }

    public string? CertificateBack { get; set; }
    public string? NationalCard { get; set; }

    public string? Avatar { get; set; }
    public string? NationalCardBack { get; set; }
    public string? Mobile { get; set; }
    public string? BankId { get; set; }
    public uint AreaId { get; set; }
    public byte? GenderId { get; set; }
    public string Address { get; set; } = null!;
}


// {
//     "first_name": "testsss",
//     "last_name": "kubaksss",
//     "mobile": "+9647700000000",
//     "gender_id": 1,
//     "national_id": "543624345",
//     "certificate_num": "r453245",
//     "certificate": "",
//     "certificate_back": "",
//     "area_id": 2,
//     "address": "ibarhim ahmet",
//     "bank_id": "",
//     "avatar": "628cc06b2579561d51907bf7",
//     "national_card": "628cc06e2579561d51907bf9",
//     "national_card_back": "628cc0722579561d51907bfb",
//     "birth_date": "2022-05-01"
// }