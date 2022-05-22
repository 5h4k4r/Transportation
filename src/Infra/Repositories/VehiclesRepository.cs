using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Core.Requests;
using Infra.Entities;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infra.Repositories;

public class VehiclesRepository : IVehiclesRepository
{


    private readonly transportationContext _context;
    private readonly IMapper _mapper;
    public VehiclesRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public Task<List<VehicleDTO>> ListVehicle(ListVehiclesRequest model)
    {
        var query = _context.Vehicles.Include(v => v.VehicleDetails).AsQueryable();

        if (model.FilterField != null && model.FilterValue != null)
        {
            switch (model.FilterField)
            {
                case ListVehicleRequestFilterField.Id:
                    query = query.Where(v => v.VehicleDetails.Any(x => x.VehicleId == Convert.ToUInt64(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.PlateNumber:
                    query = query.Where(v => v.VehicleDetails.Any(vd => vd.Plaque.Contains(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.Title:
                    query = query.Where(v => v.Title.Contains(model.FilterValue));
                    break;

                case ListVehicleRequestFilterField.Vin:
                    query = query.Where(v => v.VehicleDetails.Any(vd => vd.Vin.Contains(model.FilterValue)));
                    break;

            }
        }

        return query.ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider).ApplyPagination(model).ToListAsync();
    }

    public Task<int> ListVehicleCount(ListVehiclesRequest model)



    {
        var query = _context.Vehicles.Include(v => v.VehicleDetails).AsQueryable();

        if (model.FilterField != null && model.FilterValue != null)
        {
            switch (model.FilterField)
            {
                case ListVehicleRequestFilterField.Id:
                    query = query.Where(v => v.VehicleDetails.Any(x => x.VehicleId == Convert.ToUInt64(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.PlateNumber:
                    query = query.Where(v => v.VehicleDetails.Any(vd => vd.Plaque.Contains(model.FilterValue)));
                    break;

                case ListVehicleRequestFilterField.Title:
                    query = query.Where(v => v.Title.Contains(model.FilterValue));
                    break;

                case ListVehicleRequestFilterField.Vin:
                    query = query.Where(v => v.VehicleDetails.Any(vd => vd.Vin.Contains(model.FilterValue)));
                    break;

            }
        }
        return query.ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider).CountAsync();
    }

    public VehicleDTO AddVehicle(CreateVehicleRequest request)
    {
        var vehicle = _mapper.Map<Vehicle>(request);
        _context.Vehicles.AddAsync(vehicle);
        var vehicleDTO = _mapper.Map<VehicleDTO>(vehicle);

        return vehicleDTO;

    }

    public VehicleDetailDTO AddVehicleDetail(CreateVehicleRequest request)
    {

        var vehicleDetail = _mapper.Map<VehicleDetail>(request);
        _context.VehicleDetails.AddAsync(vehicleDetail);
        var vehicleDetailDTO = _mapper.Map<VehicleDetailDTO>(vehicleDetail);

        return vehicleDetailDTO;

    }

    public Task<List<UserDTO>> GetVehicleOwners(ulong id) =>
     _context.VehicleOwners
    .Where(v => v.VehicleId == id)
    .Include(v => v.User)
    .Select(x => x.User)
    .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
    .ToListAsync();

    public Task<List<UserDTO>> GetVehicleUsers(ulong id) =>
     _context.VehicleUsers
    .Where(v => v.VehicleId == id)
    .Include(v => v.User)
    .Select(x => x.User)
    .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
    .ToListAsync();

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