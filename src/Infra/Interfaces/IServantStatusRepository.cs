using Core.Models.Base;
using Core.Models.Requests;

namespace Infra.Interfaces;

public interface IServantStatus
{
    ServantStatusDto ChangeServantStatus(ServantStatusDto servantStatus, string status);
    Task<ServantStatusDto?> GetServantStatus(ulong servantId);
}