using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T request);

        Task<List<T>> GetAllAsync(string[]? includes = null);
        
    }
}