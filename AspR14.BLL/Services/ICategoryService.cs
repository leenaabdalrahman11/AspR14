using System.Linq.Expressions;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;

namespace AspR14.BLL.Services
{
    public interface ICategoryService
    {
        Task<Result<CategoryResponse>> CreateCategory(CategoryRequest request);

        Task<Result<List<CategoryResponse>>> GetAllCategories();
        Task<Result<CategoryResponse>> GetCategory(Expression<Func<Category, bool>> filter);
        Task<Result<bool>> DeleteCategory(int id);
    }
}