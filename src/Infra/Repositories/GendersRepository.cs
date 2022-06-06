using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class GendersRepository : IGendersRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public GendersRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GenderTranslationDto>> ListGenders(uint languageId)
    {
        var gender = _context.GenderTranslations.Where(x => x.LanguageId == languageId)
            .ProjectTo<GenderTranslationDto>(_mapper.ConfigurationProvider).ToListAsync();
        return gender;
    }
}