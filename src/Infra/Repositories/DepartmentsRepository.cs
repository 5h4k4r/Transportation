using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    protected readonly TransportationContext Context;
    private readonly IMapper _mapper;
    public DepartmentsRepository(TransportationContext context, IMapper mapper)
    {
        Context = context;
        _mapper = mapper;
    }
    public Task<DepartmentDto?> GetDepartmentById(ulong id) => Context.Departments.Where(x => x.Id == id).ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();




}
