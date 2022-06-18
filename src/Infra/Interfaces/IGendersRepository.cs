using Core.Models.Base;

namespace Infra.Interfaces;

public interface IGendersRepository
{
    Task<List<GenderTranslationDto>> ListGenders(uint languageId);
}