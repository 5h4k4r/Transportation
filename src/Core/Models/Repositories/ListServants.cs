using Core.Models.Base;

namespace Core.Models.Repositories;

public class ListServants
{
    public ServantDto servant { get; set; }
    public ServantStatusDto? servantStatus { get; set; }
}