using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Common;
using Core.Interfaces;
using Core.Models;
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

    public Task<List<VehicleDTO>> ListVehicle(PaginatedRequest model)
    {
        return _context.Vehicles.AsQueryable().ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider).ApplyPagination(model).ToListAsync();
    }

    public Task<int> ListVehicleCount(PaginatedRequest model)
    {
        return _context.Vehicles.AsQueryable().ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider).CountAsync();
    }
}