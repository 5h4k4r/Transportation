using AutoMapper;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<Category>> ListCategories()
    {
        return _context.Categories.ToListAsync();
    }

    public Task<Category> CreateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Category> UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetCategoryById(ulong id)
    {
        return _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}