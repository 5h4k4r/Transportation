using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Infra.Requests;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace Infra.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    protected transportationContext _context;
    private readonly IMapper _mapper;
    public EmployeesRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public EmployeeDTO ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model)
    {

        var databaseModel = _context.Employees.Update(new Employee { UserId = model.UserId, LanguageId = (uint)model.LanguageId!.Value });

        return _mapper.Map<EmployeeDTO>(databaseModel);
    }

    public Task<EmployeeDTO?> GetEmployeeByUserId(ulong Id)
    {

        var databaseModel = _context.Employees.Where(x => x.UserId == Id).FirstOrDefaultAsync();

        return Task.FromResult(_mapper.Map<EmployeeDTO?>(databaseModel));
    }
}