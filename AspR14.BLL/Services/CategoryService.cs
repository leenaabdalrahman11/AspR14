using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name
            };

            var createdCategory = await _categoryRepository.CreateCategoryAsync(category);

            return new CategoryResponse
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name
            };
        }

        public Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return categories.Select(category => new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();
        }
    }
}