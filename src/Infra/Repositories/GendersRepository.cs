using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class GendersRepository : IGendersRepository
{
    private readonly transportationContext _context;
    private readonly IMapper _mapper;

    public GendersRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GenderTranslationDTO>> ListGenders(uint LanguageId)
    {
        var gender = _context.GenderTranslations.Where(x => x.LanguageId == LanguageId).ProjectTo<GenderTranslationDTO>(_mapper.ConfigurationProvider).ToListAsync();
        return gender;
    }

}