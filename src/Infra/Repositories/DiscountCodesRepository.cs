using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class DiscountCodesRepository : IDiscountCodeRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public DiscountCodesRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public Task<List<DiscountCodeDto>> ListDiscountCodes(ListDiscountCodesRequest model)
    {
        var discountCodes = _context.DiscountCodes
            .ProjectTo<DiscountCodeDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();

        discountCodes = GetDiscountCodesQuery(discountCodes, model);

        return discountCodes
            .Select(d => new DiscountCodeDto
            {
                Id = d.Id,
                Code = d.Code,
                Type = d.Type,
                Value = d.Value,
                StartAt = d.StartAt,
                ExpireAt = d.ExpireAt,
                Status = d.Status,
                AreaId = d.AreaId
            })
            .ApplyPagination(model)
            .ApplySorting(model)
            .ToListAsync();
    }

    public Task<int> ListDiscountCodesCount(ListDiscountCodesRequest model)
    {
        var discountCodesQuery = _context.DiscountCodes
            .ProjectTo<DiscountCodeDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();

        discountCodesQuery = GetDiscountCodesQuery(discountCodesQuery, model);

        return discountCodesQuery.CountAsync();
    }

    public Task<DiscountCodeDto?> GetDiscountCodeDetails(uint id)
    {
        var discountCodeDetail = _context.DiscountCodes
            .Where(x => x.Id == id)
            .ProjectTo<DiscountCodeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return discountCodeDetail;
    }


    public async Task<DiscountCodeUserRepositoryDto> ListDiscountCodeUsers(ListDiscountCodesRequest model, uint codeId)
    {
        var users = await _context.DiscountCodeUsers
            .Where(x => x.DiscountCodeId == codeId)
            .Where(u => u.Used)
            .Where(m => m.ModelType == "App\\Models\\Task")
            .GroupBy(x => x.UserId)
            .Select(x => new DiscountCodeUserDto
            {
                Amount = x.Sum(t => t.Amount),
                Id = x.FirstOrDefault()!.Id,
                UserId = x.FirstOrDefault()!.UserId,
                ModelId = x.FirstOrDefault()!.ModelId,
                ModelType = x.FirstOrDefault()!.ModelType,
                count = x.Count()
            })
            .OrderBy(x => x.UserId)
            .ApplyPagination(model)
            .ApplySorting(model)
            .ToListAsync();

        var totalAmount = _context.DiscountCodeUsers
            .Where(x => x.DiscountCodeId == codeId)
            .Where(u => u.Used)
            .Where(m => m.ModelType == "App\\Models\\Task")
            .Where(u => u.Used)
            .Sum(u => u.Amount);


        var totalCount = _context.DiscountCodeUsers
            .Where(x => x.DiscountCodeId == codeId)
            .Where(u => u.Used)
            .Where(m => m.ModelType == "App\\Models\\Task")
            .Count(u => u.Used);


        var totalResult = new DiscountCodeUserRepositoryDto
        {
            TotalAmount = totalAmount,
            TotalCount = totalCount,
            DiscountCodeUser = users
        };

        return totalResult;
    }

    public Task<int> ListDiscountCodeUsersCount(ListDiscountCodesRequest model, uint codeId)
    {
        var query = _context.DiscountCodeUsers
            .Where(x => x.DiscountCodeId == codeId)
            .Where(u => u.Used)
            .Where(m => m.ModelType == "App\\Models\\Task")
            .GroupBy(x => x.UserId)
            .AsNoTracking();

        return query.CountAsync();
    }

    public async Task<DiscountCodeUserRepositoryDto> ListUserTasksByDiscountCode(ListDiscountCodesRequest model,
        uint discountCodeId,
        uint userId)
    {
        var users = await _context.DiscountCodeUsers
            .Where(x => x.DiscountCodeId == discountCodeId)
            .Where(x => x.UserId == userId)
            .Where(u => u.Used)
            .Where(m => m.ModelType == "App\\Models\\Task")
            .Select(x => new DiscountCodeUserDto
            {
                Id = x.Id,
                ModelId = x.ModelId,
                ModelType = x.ModelType,
                Amount = x.Amount,
                UserId = x.UserId
            })
            .OrderBy(x => x.UserId)
            .ApplyPagination(model)
            .ApplySorting(model)
            .ToListAsync();

        var discountCode = _context.DiscountCodes
            .Where(x => x.Id == discountCodeId)
            .Select(x => new DiscountCodeDto
            {
                Code = x.Code,
                Id = x.Id,
                Status = x.Status,
                Type = x.Type
            })
            .FirstOrDefault();
        var totalResult = new DiscountCodeUserRepositoryDto
        {
            DiscountCodeUser = users,
            DiscountCode = discountCode
        };
        return totalResult;
    }

    public async void CreateDiscountCode(DiscountCodeDto discountCode)
    {
        var newDiscountCode = _mapper.Map<DiscountCode>(discountCode);
        await _context.DiscountCodes.AddAsync(newDiscountCode);
    }


    private IQueryable<DiscountCodeDto> GetDiscountCodesQuery(IQueryable<DiscountCodeDto> query,
        ListDiscountCodesRequest model)
    {
        if (model.ActiveCodesOnly.HasValue && model.ActiveCodesOnly.Value)
            query = query
                .Where(x => x.Status == 1);

        if (model.SearchField is null || model.SearchValue is null)
            return query;
        return model.SearchField switch
        {
            "Code" => query.Where(
                x => x.Code.Contains(model.SearchValue)),
            "Type" => query.Where(
                x => x.Type.Contains(model.SearchValue))
        };
    }
}