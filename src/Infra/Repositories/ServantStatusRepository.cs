using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ServantStatusRepository : IServantStatus
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public ServantStatusRepository(TransportationContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public ServantStatusDto ChangeServantStatus(ServantStatusDto servantStatus, string status)
    {
        servantStatus.Status = status;
        var mappedModel = _mapper.Map<ServantStatus>(servantStatus);
        _context.ServantStatuses.Update(mappedModel);
        return servantStatus;
    } 
    public Task<ServantStatusDto?> GetServantStatus(ulong servantId)
    => _context.ServantStatuses
            .ProjectTo<ServantStatusDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.ServantId == servantId);
    
}