using Core.Models;
using Core.Requests;


namespace Core.Interfaces;

public interface ILanguagesRepository
{
    Task<List<LanguageDTO>> ListLanguages();
    Task<List<string>> ListLanguagesLocales();
    void CreateLanguage(CreateLanguageRequest request);
}