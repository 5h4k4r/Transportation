using AutoMapper;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class BaseTypeRepository : IBaseTypeRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public BaseTypeRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<BaseType?> GetBaseTypeById(ulong id)
    {
        return _context.BaseTypes.FirstOrDefaultAsync(x => x.Id == id);
    }
}