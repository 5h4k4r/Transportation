using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Requests;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace Infra.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    protected TransportationContext _context;
    private readonly IMapper _mapper;
    public DepartmentsRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<DepartmentDto?> GetDepartmentById(ulong Id) => _context.Departments.Where(x => x.Id == Id).ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

    public Task<List<DepartmentDto?>> GetDepartments()
    {
        var department = _context.Departments.ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider).ToListAsync();
        return department;
    }
    public async void CreateDepartment(CreatedDepartmentRequest request)
    {

        await _context.Departments.AddAsync(new Department
        {
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }
    public void DeleteDepartment(uint Id)
    {

        var department = _context.Departments.Where(x => x.Id == Id).First();
        _context.Departments.Remove(department);
    }
    public void UpdateDepartment(CreatedDepartmentRequest request, uint Id)
    {

        var department = _context.Departments.Where(x => x.Id == Id).First();

        if (department != null)
        {
            // department.Title = request.Title;
            // department.CreatedAt = request.CreatedAt;
            // department.UpdatedAt = request.UpdatedAt;
        }
    }

}
