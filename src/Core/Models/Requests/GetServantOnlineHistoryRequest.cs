using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Constants;
using Core.Interfaces;

namespace Core.Models.Requests;

public class GetServantOnlineHistoryRequest : ListServantsOnlineHistoryRequest, IPagingOptions
{
    [JsonIgnore] public double MinHours { get; set; }

    public int? Page { get; set; } = 0;

    [Range(0, CoreConstants.MaxPaginationLimit)]
    public int? Limit { get; set; } = CoreConstants.DefaultPaginationLimit;
}