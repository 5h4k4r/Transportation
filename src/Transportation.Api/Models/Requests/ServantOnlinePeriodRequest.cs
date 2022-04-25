using System.Text.Json.Serialization;
using Transportation.Api.Models.Requests;

namespace Tranportation.Api.Requests;

public class ServantOnlinePeriodRequest
{
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
}