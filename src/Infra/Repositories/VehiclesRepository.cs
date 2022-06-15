using Api.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Core.Models.Exceptions;
using Core.Models.Requests;
using Core.Models.Responses;
using Infra.Entities;
using Infra.Extensions;
using Infra.Interfaces;
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

    public Task<List<VehicleDtoResponse>> ListVehicle(ListVehiclesRequest model)
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
                        v.VehicleDetails.Any(vd =>
                            vd.Plaque != null && vd.Plaque.Contains("\"code\":\"" + model.FilterValue + "\",")));
                    break;

                case ListVehicleRequestFilterField.Title:
                    query = query.Where(v => v.Title.Contains(model.FilterValue));
                    break;

                case ListVehicleRequestFilterField.Vin:
                    query = query.Where(v =>
                        v.VehicleDetails.Any(vd => vd.Vin != null && vd.Vin.Contains(model.FilterValue)));
                    break;
            }

        return query.Select(x => new VehicleDtoResponse
            {
                Id = x.Id,
                UsageId = x.UsageId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                VehicleDetail = new VehicleDetailDtoResponse
                {
                    Id = x.VehicleDetails.FirstOrDefault()!.Id,
                    VehicleId = x.VehicleDetails.FirstOrDefault()!.VehicleId,
                    Color = x.VehicleDetails.FirstOrDefault()!.Color,
                    InsuranceExpire = x.VehicleDetails.FirstOrDefault()!.InsuranceExpire,
                    InsuranceNo = x.VehicleDetails.FirstOrDefault()!.InsuranceNo,
                    Model = x.VehicleDetails.FirstOrDefault()!.Model,
                    CreatedAt = x.VehicleDetails.FirstOrDefault()!.CreatedAt,
                    UpdatedAt = x.VehicleDetails.FirstOrDefault()!.UpdatedAt,
                    Tip = x.VehicleDetails.FirstOrDefault()!.Tip,
                    Vin = x.VehicleDetails.FirstOrDefault()!.Vin,
                    Plaque = VehicleHelper.PreparePlaque(x.VehicleDetails.FirstOrDefault()!.Plaque)
                }
            })
            //.ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(v => v.CreatedAt)
            .ApplyPagination(model).ToListAsync();
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

    public async Task<Vehicle> AddVehicle(VehicleDto vehicle)
    {
        var newVehicle = _mapper.Map<Vehicle>(vehicle);
        var dbVehicle = (await _context.Vehicles.AddAsync(newVehicle)).Entity;


        return dbVehicle;
    }

    public async Task<VehicleDetail> AddVehicleDetail(VehicleDetailDto vehicleDetail)
    {
        var newVehicleDetail = _mapper.Map<VehicleDetail>(vehicleDetail);

        await _context.VehicleDetails.AddAsync(newVehicleDetail);


        return newVehicleDetail;
    }

    public Task<VehicleDtoResponse?> GetVehicleById(ulong id)
    {
        var query = _context.Vehicles.Where(v => v.Id == id)
            .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
            .Join(_context.ServiceSubscribers, v => v.Id, ss => ss.ModelId, (vehicle, service) => new
            {
                vehicle,
                service.ServiceAreaTypeId
            })
            .Join(_context.ServiceAreaTypes, v => v.ServiceAreaTypeId, sat => sat.Id, (vs, serviceAreaType) => new
            {
                vs.vehicle,
                serviceAreaTypeId = serviceAreaType.Id,
                service = serviceAreaType.Service.Pin, // Taxi
                areaId = serviceAreaType.AreaId,
                typeId = serviceAreaType.TypeId
            })
            .Join(_context.BaseTypeTranslations.Where(x => x.LanguageId == 2),
                x => x.typeId,
                bt => bt.BaseTypeId,
                (vs, bt) => new
                {
                    vs,
                    bt.Title // gunjaw w xera
                }).Join(_context.AreaInfos, vs => vs.vs.areaId, aInfo => aInfo.AreaId, (vsa, area) => new
            {
                vsa,
                area.Title //Sulaimani
            })
            .Select(x =>
                new VehicleDtoResponse
                {
                    Id = x.vsa.vs.vehicle.Id,
                    UsageId = x.vsa.vs.vehicle.UsageId,
                    CreatedAt = x.vsa.vs.vehicle.CreatedAt,
                    UpdatedAt = x.vsa.vs.vehicle.UpdatedAt,
                    VehicleDetail = new VehicleDetailDtoResponse
                    {
                        Id = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().Id,
                        VehicleId = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().VehicleId,
                        Color = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().Color,
                        InsuranceExpire = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().InsuranceExpire,
                        InsuranceNo = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().InsuranceNo,
                        Model = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().Model,
                        CreatedAt = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().CreatedAt,
                        UpdatedAt = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().UpdatedAt,
                        Tip = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().Tip,
                        Vin = x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().Vin,
                        Plaque = VehicleHelper.PreparePlaque(x.vsa.vs.vehicle.VehicleDetails.FirstOrDefault().Plaque)
                    },
                    Services =
                        new[]
                        {
                            new ServiceResponse
                            {
                                Id = x.vsa.vs.serviceAreaTypeId,
                                Title = $"{x.vsa.vs.service} {x.vsa.Title} {x.Title}"
                            }
                        }
                }
            ).FirstOrDefault();

        return Task.FromResult(query);
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

    public Task<Vehicle> UpdateVehicle(VehicleDto vehicle)
    {
        var vehicleToUpdate = _mapper.Map<Vehicle>(vehicle);
        _context.Vehicles.Update(vehicleToUpdate);
        return Task.FromResult(vehicleToUpdate);
    }

    public async Task AddServantToVehicle(ulong vehicleId, ulong servantUserId)
    {
        var vehicle = _context.Vehicles.SingleOrDefault(v => v.Id == vehicleId);
        var servant = _context.Servants.SingleOrDefault(s => s.UserId == servantUserId);

        if (vehicle is null || servant is null)
            throw new NotFoundException("Vehicle or Servant not found");

        var dbVehicleUser = new VehicleUser
        {
            VehicleId = vehicleId,
            UserId = servantUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var dbVehicleOwner = new VehicleOwner
        {
            VehicleId = vehicleId,
            UserId = servantUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var vehicleUser =
            _context.VehicleUsers
                .SingleOrDefault(v => v.VehicleId == vehicleId && v.UserId == servantUserId);

        if (vehicleUser is null)
        {
            await _context.VehicleUsers.AddAsync(dbVehicleUser);
            await _context.VehicleOwners.AddAsync(dbVehicleOwner);
        }
        else
        {
            throw new DuplicateException("Record Already Exists");
        }
    }

    public Task DeleteVehicle(ulong id)
    {
        var dbVehicle = _context.Vehicles.SingleOrDefaultAsync(v => v.Id == id).Result;
        if (dbVehicle is null)
            throw new NotFoundException("Record Not Found");

        dbVehicle.DeletedAt = DateTime.UtcNow;
        return Task.CompletedTask;
    }

    public async Task SubscribeVehicleToService(ulong vehicleId, ICollection<ulong> serviceIds)
    {
        foreach (var serviceId in serviceIds)
        {
            var vehicleService = new ServiceSubscriber
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsSubscribed = true,
                ModelType = "App\\Models\\Vehicle",
                ModelId = vehicleId,
                ServiceAreaTypeId = serviceId
            };
            await _context.ServiceSubscribers.AddAsync(vehicleService);
        }
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