using Core.Repositories;

namespace Infra.Repositories;

public class ServantPerformanceWithUserResponse
{
    public ServantPerformance? Performance { get; set; }
    public ServantPerformed? Servant { get; set; }
}