using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;

namespace AspR14.BLL.Services
{
    public interface ICartService
    {
        public Task<Result<bool>> AddToCart(string userId, CartItemRequest request);
        public Task<Result<List<CartItemResponse>>> GetCartItems(string userId);
        public Task<Result<bool>> RemoveItem(string userId,int productId);
        public Task<Result<bool>> UpdateItem(string userId,int productId, int Count);
        
    }
}