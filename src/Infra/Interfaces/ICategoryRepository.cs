using Infra.Entities;

namespace Infra.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> ListCategories();

    Task<Category?> GetCategoryById(ulong id);

    Task<Category> CreateCategory(Category category);

    Task<Category> UpdateCategory(Category category);
}