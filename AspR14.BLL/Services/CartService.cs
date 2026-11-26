using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;
using AspR14.DAL.Repositories;
using Azure;
using Mapster;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspR14.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> AddToCart(string userId, CartItemRequest request)
        {
            var product = await _unitOfWork.ProductRepository.GetOne(p => p.Id == request.ProductId);
            if (product == null)
            {
                return Result<bool>.Fail("Product not found.");
            }
            else if (request.Count <= 0)
            {
                return Result<bool>.Fail("Count must be greater than zero.");
            }
            else if (request.Count > product.Quantity)
            {
                return Result<bool>.Fail("Count exceeds available stock.");
            }
            var existingCartItem = await _unitOfWork.CartRepository.GetOne(c => c.UserId == userId && c.ProductId == request.ProductId);
            if (existingCartItem != null)
            {
                existingCartItem.Count += request.Count;
            }
            else
            {
                var item = new CartItem
                {
                    UserId = userId,
                    Count = request.Count,
                    ProductId = request.ProductId
                };
                await _unitOfWork.CartRepository.CreateAsync(item);
            }
            await _unitOfWork.CompleteAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<List<CartItemResponse>>> GetCartItems(string userId)
        {
            var cartItems = await _unitOfWork.CartRepository.GetAllAsync(
                filter: c => c.UserId == userId,
                includes: new string[]
                {
                    nameof(CartItem.Product),
                    $"{nameof(CartItem.Product)}.{nameof(Product.Translations)}"
                }
            );
            if (cartItems is null)
            {
                return Result<List<CartItemResponse>>.Fail("cart item not found");
            }
            var response = cartItems.Adapt<List<CartItemResponse>>();

            return Result<List<CartItemResponse>>.Ok(response);
        }
        public async Task<Result<bool>> RemoveItem(string userId, int productId)
        {
            var cartItem = await _unitOfWork.CartRepository.GetOne(
                filter: c => c.UserId == userId && c.ProductId == productId
            );
            if (cartItem is null)
            {
                return Result<bool>.Fail("Cart item not found");
            }
            _unitOfWork.CartRepository.Delete(cartItem);
            var completed = await _unitOfWork.CompleteAsync();
            return Result<bool>.Ok(completed > 0, completed > 0 ? "Cart Item deleted successfully." : "Failed to delete Cart Item.");

        }
        public async Task<Result<bool>> UpdateItem(string userId, int productId, int Count)
        {
            var cartItem = await _unitOfWork.CartRepository.GetOne(
                filter: c => c.UserId == userId && c.ProductId == productId,
                includes: new string[]
            {
                nameof(CartItem.Product)
            }
            );
            if (cartItem is null)
            {
                return Result<bool>.Fail("Cart item not found");
            }
            if (Count <= 0)
            {
                return Result<bool>.Fail("Count must be greater than 0");
            }
            if (Count > cartItem.Product.Quantity)
            {
                return Result<bool>.Fail("Insufficient stock");
            }
            cartItem.Count = Count;
            var completed = await _unitOfWork.CompleteAsync();
            return Result<bool>.Ok(completed > 0, completed > 0 ? "Cart item count updated successfully." : "Failed to update cart item count.");

        }
    }
}