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

    public Task<List<ListServicesResponses>> ListServices(ulong? languageId = 2)
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
                                        .FirstOrDefault(x => x.LanguageId == languageId)!.Id,
                                    CategoryId = s.ServiceAreaTypes.FirstOrDefault()!.Category.CategoryTranslations
                                        .FirstOrDefault(x => x.LanguageId == languageId)!.CategoryId,
                                    LanguageId = s.ServiceAreaTypes.FirstOrDefault().Category.CategoryTranslations
                                        .FirstOrDefault(x => x.LanguageId == languageId)!.LanguageId,
                                    Title = s.ServiceAreaTypes.FirstOrDefault()!.Category.CategoryTranslations
                                        .FirstOrDefault(x => x.LanguageId == languageId)!.Title
                                }
                            }
                        }
                    }
                }
            }).ToList();

        return Task.FromResult(services);
    }

    public Task<ServiceAreaType?> GetServiceById(ulong id)
    {
        var query = _context.ServiceAreaTypes.Where(x => x.Id == id).AsNoTracking();

        var serviceAreaType = query.Select(s => new ServiceAreaType
        {
            Id = s.Id,
            AreaId = s.AreaId,
            ServiceId = s.ServiceId,
            Params = s.Params,
            Service = new Service
            {
                Id = s.Service.Id,
                Pin = s.Service.Pin,
                CreatedAt = s.Service.CreatedAt,
                UpdatedAt = s.Service.UpdatedAt
            },
            Category = new Category
            {
                Id = s.Category.Id,
                CategoryTranslations = new List<CategoryTranslation>
                {
                    new()
                    {
                        Id = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.Id,
                        CategoryId = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.CategoryId,
                        LanguageId = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.LanguageId,
                        Title = s.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == 2)!.Title
                    }
                }
            },
            Commissions = new List<Commission>
            {
                new()
                {
                    Id = s.Commissions.FirstOrDefault(x => x.DeletedAt == null)!.Id,
                    Value = s.Commissions.FirstOrDefault(x => x.DeletedAt == null)!.Value,
                    IsWithdrawFromGift = s.Commissions.FirstOrDefault(x => x.DeletedAt == null)!.IsWithdrawFromGift
                }
            },
            Discounts = new List<Discount>
            {
                new()
                {
                    Id = s.Discounts.FirstOrDefault(x => x.DeletedAt == null)!.Id,
                    Value = s.Discounts.FirstOrDefault(x => x.DeletedAt == null)!.Value,
                    Limit = s.Discounts.FirstOrDefault(x => x.DeletedAt == null)!.Limit,
                    Periods = s.Discounts.FirstOrDefault(x => x.DeletedAt == null)!.Periods
                }
            }
        }).FirstOrDefault();

        return Task.FromResult(serviceAreaType);
    }

    public Task<ServiceAreaType> CreateServiceAreaType(ServiceAreaTypeDto serviceAreaType)
    {
        var serviceAreaTypeEntity = _mapper.Map<ServiceAreaType>(serviceAreaType);
        var service = _context.ServiceAreaTypes.Add(serviceAreaTypeEntity);

        return Task.FromResult(service.Entity);
    }

    public Task<ServiceAreaType?> GetServiceAreaTypeById(uint id)
    {
        var serviceAreaType = _context.ServiceAreaTypes.Where(x => x.Id == id)
            .Include(x => x.Commissions.Where(c => c.DeletedAt == null))
            .Include(x => x.Discounts.Where(d => d.DeletedAt == null)).FirstOrDefault();
        return Task.FromResult(serviceAreaType);
    }

    public Task<ServiceAreaType> UpdateServiceAreaType(ServiceAreaType serviceAreaType)
    {
        var updatedEntity = _context.ServiceAreaTypes.Update(serviceAreaType);
        return Task.FromResult(updatedEntity.Entity);
    }
}