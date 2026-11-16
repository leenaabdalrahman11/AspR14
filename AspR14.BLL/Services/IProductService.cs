using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;

namespace AspR14.BLL.Services
{
    public interface IProductService
    {
        Task<Result<ProductResponse>> GetProductByIdAsync(Expression<Func<Product, bool>> predicate);
        Task<Result<List<ProductResponse>>> GetAllProductsAsync();
        Task<Result<ProductResponse>> CreateProductAsync(ProductRequest productRequest);
        Task<Result<ProductResponse>> UpdateProductAsync(int productId, ProductRequest productRequest);
        Task<Result<bool>> DeleteProductAsync(Expression<Func<Product, bool>> predicate);
    }
}