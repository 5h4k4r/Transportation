using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tranportation.Api.Requests;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Tranportation.Api.Repositories;


public class LanguagesRepository : ILanguagesRepository
{
    private readonly transportationContext _context;

    public LanguagesRepository(transportationContext context)
    {
        _context = context;
    }

    public ValueTask<EntityEntry<Language>> CreateLanguage(CreateLanguageRequest request)
    {
        return _context.Languages.AddAsync(new Language
        {
            Title = request.Title,
            Locale = request.Locale,
            Direction = request.Direction,
        });

    }

    public Task<List<Language>> ListLanguages() => _context.Languages.ToListAsync();

    public Task<List<string>> ListLanguagesLocales() => _context.Languages.Select(x => x.Locale).ToListAsync();



}