using Core.Models;
using Infra.Requests;


namespace Core.Interfaces;

public interface ILanguagesRepository
{
    Task<List<LanguageDTO>> ListLanguages();
    Task<List<string>> ListLanguagesLocales();
    void CreateLanguage(CreateLanguageRequest request);
}