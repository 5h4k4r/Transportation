using Core.Models.Base;
using Core.Models.Requests;

namespace Infra.Interfaces;

public interface ILanguagesRepository
{
    Task<List<LanguageDto>> ListLanguages();
    Task<List<string>> ListLanguagesLocales();
    void CreateLanguage(CreateLanguageRequest request);
}