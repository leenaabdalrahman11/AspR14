using System.Collections.Generic;
using System.Threading.Tasks;
using AspR14.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AspR14.DAL.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T>
 where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<List<T>> GetAllAsync(string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();//Store the data in the server's RAM and wait,Jaida may edit it later 
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
                return await query.ToListAsync();
                
            }
            //var data = _context.Set<T>().ToListAsync();// give me data from Db and put them on user ram
            return await query.ToListAsync();
        }
    }
}