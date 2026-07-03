using System;
using System.Collections.Generic;
using System.Text;
using AspR14.DAL.Data;
using AspR14.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AspR14.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}