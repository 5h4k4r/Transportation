using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class VehiclesRepository : IVehiclesRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public VehiclesRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<VehicleDto>> ListVehicle(ListVehiclesRequest model)
    {
        var query = _context.Vehicles.Include(v => v.VehicleDetails).AsQueryable();

        if (model.FilterField != null && model.FilterValue != null)
            switch (model.FilterField)
            {
                case ListVehicleRequestFilterField.Id:
                    query = query.Where(v =>
                        v.VehicleDetails.Any(x => x.VehicleId == Convert.ToUInt64(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.PlateNumber:
                    query = query.Where(v =>
                        v.VehicleDetails.Any(vd => vd.Plaque != null && vd.Plaque.Contains(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.Title:
                    query = query.Where(v => v.Title.Contains(model.FilterValue));
                    break;

                case ListVehicleRequestFilterField.Vin:
                    query = query.Where(v =>
                        v.VehicleDetails.Any(vd => vd.Vin != null && vd.Vin.Contains(model.FilterValue)));
                    break;
            }

        return query.ProjectTo<VehicleDto>(_mapper.ConfigurationProvider).ApplyPagination(model).ToListAsync();
    }

    public Task<int> ListVehicleCount(ListVehiclesRequest model)


    {
        var query = _context.Vehicles.Include(v => v.VehicleDetails).AsQueryable();

        if (model.FilterField != null && model.FilterValue != null)
            switch (model.FilterField)
            {
                case ListVehicleRequestFilterField.Id:
                    query = query.Where(v =>
                        v.VehicleDetails.Any(x => x.VehicleId == Convert.ToUInt64(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.PlateNumber:
                    query = query.Where(v =>
                        v.VehicleDetails.Any(vd => vd.Plaque != null && vd.Plaque.Contains(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.Title:
                    query = query.Where(v => v.Title.Contains(model.FilterValue));
                    break;

                case ListVehicleRequestFilterField.Vin:
                    query = query.Where(v =>
                        v.VehicleDetails.Any(vd => vd.Vin != null && vd.Vin.Contains(model.FilterValue)));
                    break;
            }

        return query.ProjectTo<VehicleDto>(_mapper.ConfigurationProvider).CountAsync();
    }

    public async void AddVehicle(VehicleDto vehicle)
    {
        var newVehicle = _mapper.Map<Vehicle>(vehicle);
        await _context.Vehicles.AddAsync(newVehicle);
    }

    public async void AddVehicleDetail(VehicleDetailDto vehicleDetail)
    {
        var newVehicleDetail = _mapper.Map<VehicleDetail>(vehicleDetail);
        await _context.VehicleDetails.AddAsync(newVehicleDetail);
    }

    public Task<VehicleDto?> GetVehicleById(ulong id)
    {
        return _context.Vehicles.Where(v => v.Id == id).ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }


    public Task<List<UserDto>> GetVehicleOwners(ulong id)
    {
        return _context.VehicleOwners
            .Where(v => v.VehicleId == id)
            .Include(v => v.User)
            .Select(x => x.User)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public Task<List<UserDto>> GetVehicleUsers(ulong id)
    {
        return _context.VehicleUsers
            .Where(v => v.VehicleId == id)
            .Include(v => v.User)
            .Select(x => x.User)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public Task UpdateVehicle(VehicleDto vehicle)
    {
        var vehicleToUpdate = _mapper.Map<Vehicle>(vehicle);
        _context.Vehicles.Update(vehicleToUpdate);
        return Task.CompletedTask;
    }
}


//     {
//     "title": "Toyota",
//     "color": "hard",
//     "model": "2010-11-17",
//     "tip": "crolla",
//     "insurance_no": "",
//     "insurance_expire": "2023-05-01",
//     "vin": "536543735vh",
//     "usage_id": 1,
//     "plaque": "{\"city\":\"slemani\",\"code\":\"55961\",\"text\":\"\",\"color\":\"red\",\"country\":\"iraq\",\"textr\":\"white\"}",
//     "options": [],
//     "service_area_types": [
//         2
//     ],
//     "car_card": "6285e12d2579561d51907b93",
//     "car_card_back": "6285e1432579561d51907b95",
//     "tech_diagnosis": "",
//     "insurance": ""
// }