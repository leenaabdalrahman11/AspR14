using System.Linq.Expressions;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;

namespace AspR14.BLL.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateCategory(CategoryRequest request);

        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> GetCategory(Expression<Func<Category, bool>> filter);
        Task<bool> DeleteCategory(int id);
    }
}