using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    protected TransportationContext _Context;
    private readonly IMapper _mapper;
    public EmployeesRepository(TransportationContext context, IMapper mapper)
    {
        _Context = context;
        _mapper = mapper;
    }

    public EmployeeDto ChangeEmployeeLanguage(ChangeEmployeeLanguageRequest model)
    {

        var databaseModel = _Context.Employees.Update(new Employee { UserId = model.UserId, LanguageId = (uint)model.LanguageId!.Value });

        return _mapper.Map<EmployeeDto>(databaseModel);
    }

    public Task<EmployeeDto?> GetEmployeeByUserId(ulong id) =>
         _Context.Employees.Where(x => x.UserId == id).ProjectTo<EmployeeDto?>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();


}