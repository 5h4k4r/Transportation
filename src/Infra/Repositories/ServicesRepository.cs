using AutoMapper;
using Core.Models.Base;
using Core.Models.Responses;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class ServicesRepository : IServiceRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public ServicesRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<ListServicesResponses>> ListServices()
    {
        var services = _context.Services
            .Include(x => x.ServiceAreaTypes)
            .ThenInclude(x => x.Category)
            .ThenInclude(x => x.CategoryTranslations)
            .Select(s => new ListServicesResponses
            {
                Id = s.Id,
                Pin = s.Pin,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                ServiceAreaTypes = new List<ServiceAreaTypeDtoResponse>
                {
                    new()
                    {
                        Id = s.ServiceAreaTypes.FirstOrDefault()!.Id,
                        AreaId = s.ServiceAreaTypes.FirstOrDefault()!.AreaId,
                        Category = new CategoryDto
                        {
                            Id = s.ServiceAreaTypes.FirstOrDefault()!.Category.Id,
                            CategoryTranslations = new List<CategoryTranslationDto>
                            {
                                new()
                                {
                                    Id = s.ServiceAreaTypes.FirstOrDefault()!.Category.CategoryTranslations
                                        .FirstOrDefault(x => x.LanguageId == 2)!.Id,
                                    CategoryId = s.ServiceAreaTypes.FirstOrDefault()!.Category.CategoryTranslations
                                        .FirstOrDefault(x => x.LanguageId == 2)!.CategoryId,
                                    LanguageId = s.ServiceAreaTypes.FirstOrDefault().Category.CategoryTranslations
                                        .FirstOrDefault(x => x.LanguageId == 2)!.LanguageId,
                                    Title = s.ServiceAreaTypes.FirstOrDefault()!.Category.CategoryTranslations
                                        .FirstOrDefault(x => x.LanguageId == 2)!.Title
                                }
                            }
                        }
                    }
                }
            }).ToList();

        return Task.FromResult(services);
    }

    public Task<ServiceAreaTypeDto?> GetServiceById(uint id, uint? serviceId = null)
    {
        var query = _context.ServiceAreaTypes.AsQueryable().Where(x => x.Id == id).AsNoTracking();

        if (serviceId.HasValue)
            query = query.Where(x => x.ServiceId == serviceId);

        var serviceAreaType = query.Select(s => new ServiceAreaTypeDto
        {
            Id = s.Id,
            AreaId = s.AreaId,
            ServiceId = s.ServiceId,
            Params = s.Params,
            Service = new ServiceDto
            {
                Id = s.Service.Id,
                Pin = s.Service.Pin,
                CreatedAt = s.Service.CreatedAt,
                UpdatedAt = s.Service.UpdatedAt
            },
            Category = new CategoryDto
            {
                Id = s.Category.Id,
                CategoryTranslations = new List<CategoryTranslationDto>
                {
                    new()
                    {
                        Id = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.Id,
                        CategoryId = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.CategoryId,
                        LanguageId = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.LanguageId,
                        Title = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.Title
                    }
                }!
            }
        }).FirstOrDefault();

        return Task.FromResult(serviceAreaType);
    }
}