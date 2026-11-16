using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.Data;
using AspR14.DAL.Models;

namespace AspR14.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}