using Core.Models.Base;


namespace Core.Interfaces;

public interface IGendersRepository
{
    Task<List<GenderTranslationDto>> ListGenders(uint languageId);
}