using System;
using System.Collections.Generic;
using System.Text;

using AspR14.DAL.Models;

namespace AspR14.DAL.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<List<Category>> GetAllCategoriesAsync();
    }
}