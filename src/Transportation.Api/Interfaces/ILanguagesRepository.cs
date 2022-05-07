using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tranportation.Api.Requests;
using Transportation.Api.Model;

namespace Transportation.Api.Interfaces;

public interface ILanguagesRepository
{
    Task<List<Language>> ListLanguages();
    Task<List<string>> ListLanguagesLocales();
    ValueTask<EntityEntry<Language>> CreateLanguage(CreateLanguageRequest request);
}