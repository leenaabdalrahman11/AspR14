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
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<CategoryResponse>>> GetAllCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(

              new string[] { nameof(Category.Translations), nameof(Category.CreatedBy) }
          );
            return Result<List<CategoryResponse>>.Ok(categories.Adapt<List<CategoryResponse>>(), "Successfully retrieved all categories.");
        }

        public async Task<Result<CategoryResponse>> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var category = await _unitOfWork.CategoryRepository.GetOne(filter, new string[] { nameof(Category.Translations), nameof(Category.CreatedBy) });

            if (category == null)
            {
                return Result<CategoryResponse>.Fail("Category not found.");
            }
            return Result<CategoryResponse>.Ok(category.Adapt<CategoryResponse>(), "Successfully retrieved the category.");

        }

        public async Task<Result<CategoryResponse>> CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();

            await _unitOfWork.CategoryRepository.CreateAsync(category);
            if (category == null)
            {
                return Result<CategoryResponse>.Fail("Failed to create category.");
            }
            return Result<CategoryResponse>.Ok(category.Adapt<CategoryResponse>(), "Category created successfully.");


        }
        public async Task<Result<bool>> DeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetOne(c => c.Id == id);
            if (category == null)
            {
                return Result<bool>.Fail("Category not found.");
            }
            _unitOfWork.CategoryRepository.Delete(category);
            var completed = await _unitOfWork.CompleteAsync();
            return Result<bool>.Ok(completed > 0, completed > 0 ? "Category deleted successfully." : "Failed to delete category.");
        }
    }
}