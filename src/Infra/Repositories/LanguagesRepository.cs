using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;


public class LanguagesRepository : ILanguagesRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public LanguagesRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async void CreateLanguage(CreateLanguageRequest request)
    {
        await _context.Languages.AddAsync(new Language
        {
            Title = request.Title,
            Locale = request.Locale,
            Direction = request.Direction,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }

    public Task<List<LanguageDto>> ListLanguages() => _context.Languages.ProjectTo<LanguageDto>(_mapper.ConfigurationProvider).ToListAsync();

    public Task<List<string>> ListLanguagesLocales() => _context.Languages.Select(x => x.Locale).ToListAsync();



}