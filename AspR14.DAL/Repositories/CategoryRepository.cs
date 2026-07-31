using AspR14.DAL.Data;
using AspR14.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AspR14.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}