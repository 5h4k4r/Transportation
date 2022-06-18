using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models.Requests;

public class NewJobRequest
{
    [Required] public int Id { get; set; }
    public LocationPoint? Origin { get; set; }
    public List<LocationPoint>? Destination { get; set; }
    [Required] public long Price { get; set; }
    public long PriceDifference { get; set; }
    public int DiscountCodeId { get; set; }
    public int? Reserve { get; set; }
    public int? TwoWay { get; set; }
    public List<string>? Date { get; set; }
    public List<string>? Time { get; set; }
    public List<string>? Members { get; set; }

    // Comment Reason: Not used in laravel jobRequest services 
    // public int Requirements { get; set; } 
    public int? SelectedOptionId { get; set; }
    public string? PaymentType { get; set; }
}

public abstract class LocationPoint
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}