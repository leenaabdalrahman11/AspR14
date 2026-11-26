using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspR14.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AspR14.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
     where T : class // T is a reference type, not a struct or primitive type
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T request)
        {
            await _context.AddAsync(request);
            return request;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T ,bool>>? filter= null , string[]? includes = null)
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
            if(filter is not null)
            {
                query = query.Where(filter);
            }
            //var data = _context.Set<T>().ToListAsync();// give me data from Db and put them on user ram
            return await query.ToListAsync();
        }
        public async Task<T> GetOne(Expression<Func<T ,bool>> filter, string[]? includes=null)
        {
            IQueryable<T> query = _context.Set<T>();//Store the data in the server's RAM and wait,Jaida may edit it later 
           if (includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
                
            }
            return await query.FirstOrDefaultAsync(filter);
        }
        public async Task<bool> UpdateAsync(T request)
        {
            _context.Update(request);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public void Delete(T request)
        {
            _context.Remove(request);
        }
    }
}