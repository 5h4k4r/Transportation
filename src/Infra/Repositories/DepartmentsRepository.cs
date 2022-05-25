using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    protected TransportationContext _Context;
    private readonly IMapper _mapper;
    public DepartmentsRepository(TransportationContext context, IMapper mapper)
    {
        _Context = context;
        _mapper = mapper;
    }
    public Task<DepartmentDto?> GetDepartmentById(ulong id) => _Context.Departments.Where(x => x.Id == id).ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();




}
