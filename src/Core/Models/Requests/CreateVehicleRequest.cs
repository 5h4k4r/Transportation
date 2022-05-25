using System.Text.Json.Serialization;

namespace Core.Requests;


public class CreateVehicleRequest
{
    public string Title { get; set; } = string.Empty;
    public ulong UsageId { get; set; }
    public string Color { get; set; } = string.Empty;

    public string Model { get; set; }

    public string Tip { get; set; } = string.Empty;

    public string InsuranceNo { get; set; } = string.Empty;

    public DateOnly? InsuranceExpire { get; set; }

    public string Vin { get; set; } = string.Empty;


    public string Plaque { get; set; } = string.Empty;

    public ICollection<ulong> ServiceAreaTypes { get; set; }

    public ICollection<ulong> Options { get; set; }

    public string CarCard { get; set; } = string.Empty;

    public string CarCardBack { get; set; } = string.Empty;

    public string TechInsuransis { get; set; } = string.Empty;

    public string Incurance { get; set; } = string.Empty;
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

//     {
//     "title": "Toyota",
//     "color": "hard",
//     "model": "2010-11-17",
//     "tip": "crolla",
//     "insurance_no": "",
//     "insurance_expire": "2023-05-01",
//     "vin": "536543735vh",
//     "usage_id": 1,
//     "plaque": "{\"city\":\"slemani\",\"code\":\"55961\",\"text\":\"\",\"color\":\"red\",\"country\":\"iraq\",\"textr\":\"white\"}",
//     "options": [],
//     "service_area_types": [
//         2
//     ],
//     "car_card": "6285e12d2579561d51907b93",
//     "car_card_back": "6285e1432579561d51907b95",
//     "tech_diagnosis": "",
//     "insurance": ""
// }