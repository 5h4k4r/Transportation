using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public EmployeesRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public EmployeeDto ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model)
    {
        var databaseModel = _context.Employees.Update(new Employee
            { UserId = model.UserId, LanguageId = (uint)model.LanguageId!.Value });

        return _mapper.Map<EmployeeDto>(databaseModel);
    }

    public Task<EmployeeDto?> GetEmployeeByUserId(ulong id)
    {
        return _context.Employees.Where(x => x.UserId == id).ProjectTo<EmployeeDto?>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}