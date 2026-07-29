using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;

namespace AspR14.BLL.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateCategory(CategoryRequest request);

        Task<List<CategoryResponse>> GetAllCategories();
    }
}