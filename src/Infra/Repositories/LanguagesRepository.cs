using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models;
using Core.Requests;
using Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace Infra.Repositories;


public class LanguagesRepository : ILanguagesRepository
{
    private readonly transportationContext _context;
    private readonly IMapper _mapper;

    public LanguagesRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public LanguageDTO CreateLanguage(CreateLanguageRequest request)
    {

        var databaseModel = _context.Languages.AddAsync(new Language
        {
            Title = request.Title,
            Locale = request.Locale,
            Direction = request.Direction,
        });

        return _mapper.Map<LanguageDTO>(databaseModel);

    }

    public Task<List<LanguageDTO>> ListLanguages() => _context.Languages.ProjectTo<LanguageDTO>(_mapper.ConfigurationProvider).ToListAsync();

    public Task<List<string>> ListLanguagesLocales() => _context.Languages.Select(x => x.Locale).ToListAsync();



}