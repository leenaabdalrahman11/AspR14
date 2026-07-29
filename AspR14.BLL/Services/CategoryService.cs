using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;
using AspR14.DAL.Repositories;

namespace AspR14.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(category => new CategoryResponse
            {
                Id = category.Id,
                Name = category.Translations
                    .FirstOrDefault(x => x.Language == "en")?.Name
                    ?? string.Empty
            }).ToList();
        }

        async Task<CategoryResponse> ICategoryService.CreateCategory(CategoryRequest request)
        {
            var category = new Category
            {
                Translations = new List<CategoryTranslation>
                {
                    new CategoryTranslation
                    {
                        Name = request.Name,
                        Language = "en"
                    }
                }
            };

            await _categoryRepository.CreateAsync(category);

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Translations
                    .FirstOrDefault(x => x.Language == "en")?.Name
                    ?? string.Empty
            };
        }
    }
}