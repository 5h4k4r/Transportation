using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly IMapper _mapper;
    protected readonly TransportationContext Context;

    public DepartmentsRepository(TransportationContext context, IMapper mapper)
    {
        Context = context;
        _mapper = mapper;
    }

    public Task<DepartmentDto?> GetDepartmentById(ulong id)
    {
        return Context.Departments.Where(x => x.Id == id).ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}
