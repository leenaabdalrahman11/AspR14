using System;
using System.Collections.Generic;
using System.Text;

using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;

namespace AspR14.BLL.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);

        Task<List<CategoryResponse>> GetAllCategoriesAsync();
    }
}