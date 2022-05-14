using Core.Models;


namespace Core.Interfaces;

public interface IGendersRepository
{
    Task<List<GenderTranslationDTO>> ListGenders(uint LanguageId);
}