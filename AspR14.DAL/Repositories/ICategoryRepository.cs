using AspR14.DAL.Models;

namespace AspR14.DAL.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<List<Category>> GetAllAsync();
    }
}