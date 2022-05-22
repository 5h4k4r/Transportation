using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Core.Requests;
using Infra.Entities;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;

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
}