using Core.Models.Repositories;

namespace Infra.Models.Responses;

public class ServantPerformanceWithUserResponse
{
    public ServantPerformance? Performance { get; set; }
    public ServantPerformed? Servant { get; set; }
}