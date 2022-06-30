using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Models.Requests;

public class ListServantPerformancesRequest : GetServantPerformanceRequest,IPagingOptions
{ public int? Page { get; set; }
    [Range(0, Constants.CoreConstants.MaxPaginationLimit)]
    public int? Limit { get; set; }
}
