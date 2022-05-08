using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace Infra.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    protected transportationContext _context;
    private readonly IMapper _mapper;
    public DepartmentsRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<DepartmentDTO?> GetDepartmentById(ulong Id)
    {
        var databaseModel = _context.Departments.Where(x => x.Id == Id).FirstOrDefaultAsync();

        return Task.FromResult(_mapper.Map<DepartmentDTO?>(databaseModel));
    }

    

}
