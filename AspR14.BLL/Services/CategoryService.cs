using System.Globalization;
using System.Linq.Expressions;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;
using AspR14.DAL.Repositories;
using Mapster;

namespace AspR14.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<List<CategoryResponse>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync(

              new string[] { nameof(Category.Translations), nameof(Category.CreatedBy) }
          );
            return new Result<List<CategoryResponse>>
            {
                IsSuccess = true,
                Message = "Successfully retrieved categories.",
                Data = categories.Adapt<List<CategoryResponse>>()
            };

        }

        public async Task<Result<CategoryResponse>> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var category = await _categoryRepository.GetOne(filter, new string[] { nameof(Category.Translations), nameof(Category.CreatedBy) });
            if (category == null)
            {
                return new Result<CategoryResponse>
                {
                    IsSuccess = false,
                    Message = "Category not found."
                };
            }
            return new Result<CategoryResponse>
            {
                IsSuccess = true,
                Message = "Successfully retrieved category.",
                Data = category.Adapt<CategoryResponse>()
            };

        }

        async Task<Result<CategoryResponse>> ICategoryService.CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();

            await _categoryRepository.CreateAsync(category);
            if (category == null)
            {
                return new Result<CategoryResponse>
                {
                    IsSuccess = false,
                    Message = "Failed to create category."
                };
            }
            return new Result<CategoryResponse>
            {
                IsSuccess = true,
                Message = "Category created successfully.",
                Data = category.Adapt<CategoryResponse>()
            };


        }
        public async Task<Result<bool>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetOne(c => c.Id == id);
            if (category == null)
            {
                return new Result<bool>
                {
                    IsSuccess = false,
                    Message = "Category not found."
                };
            }
            var result = await _categoryRepository.DeleteAsync(category);
            return new Result<bool>
            {
                IsSuccess = result,
                Message = result ? "Category deleted successfully." : "Failed to delete category.",
                Data = result
            };
        }
    }
}