using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.Data;
using AspR14.DAL.Repositories;


namespace AspR14.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ICategoryRepository? _categoryRepository;
        private IProductRepository? _productRepository;
        private ICartRepository? _cartRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository is null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }

                return _categoryRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository is null)
                {
                    _productRepository = new ProductRepository(_context);
                }

                return _productRepository;
            }
        }
        public ICartRepository CartRepository
        {
            get
            {
                if (_cartRepository is null)
                {
                    _cartRepository = new CartRepository(_context);
                }

                return _cartRepository;
            }
        }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
