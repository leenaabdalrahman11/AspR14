using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;
using AspR14.DAL.Repositories;
using Mapster;

namespace AspR14.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<Result<ProductResponse>> CreateProductAsync(ProductRequest productRequest)
        {
            if (productRequest.MainImage == null)
            {
                return Result<ProductResponse>.Fail("Main image is required for product creation.");
            }
            var ImageUploaded = await _fileService.UploadFileAsync(productRequest.MainImage);
            if (!ImageUploaded.IsSuccess)
            {
                return Result<ProductResponse>.Fail($"Main image upload failed: {ImageUploaded.Message}");
            }
            var subImageNames = new List<string>();

            if (productRequest.SubImages != null)
            {
                foreach (var image in productRequest.SubImages)
                {
                    var result = await _fileService.UploadFileAsync(image);

                    if (!result.IsSuccess || result.Data == null)
                    {
                        /*return new Result<ProductResponse>
                        {
                            IsSuccess = false,
                            Message = $"Sub image upload failed: {result.Message}"
                        };*/
                        return Result<ProductResponse>.Fail($"Sub image upload failed: {result.Message}");
                    }

                    subImageNames.Add(result.Data);
                }
            }

            var product = productRequest.Adapt<Product>();

            product.MainImage = ImageUploaded.Data;
            product.SubImages = subImageNames;
            var createdProduct = await _productRepository.CreateAsync(product);
            return Result<ProductResponse>.Ok(createdProduct.Adapt<ProductResponse>(), "Product created successfully.");

        }

        public async Task<Result<bool>> DeleteProductAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await _productRepository.GetOne(predicate);
            if (product == null)
            {
                return Result<bool>.Fail("Product not found.");
            }

            var result = await _productRepository.DeleteAsync(product);
            return Result<bool>.Ok(result, result ? "Product deleted successfully." : "Failed to delete product.");

        }

        public async Task<Result<List<ProductResponse>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync(new string[]
            {
                    nameof(Product.Category),
                    nameof(Product.Translations)
            });
            return Result<List<ProductResponse>>.Ok(products.Adapt<List<ProductResponse>>(), "Products retrieved successfully.");

        }

        public async Task<Result<ProductResponse>> GetProductByIdAsync(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetOne(filter, new string[]
            {
                    nameof(Product.Category),
                    nameof(Product.Translations)
            });

            if (product == null)
            {
                return Result<ProductResponse>.Fail("Product not found.");
            }

            return Result<ProductResponse>.Ok(product.Adapt<ProductResponse>(), "Product retrieved successfully.");

        }

        public async Task<Result<ProductResponse>> UpdateProductAsync(
            int productId,
            ProductRequest productRequest)
        {
            var product = await _productRepository.GetOne(
                p => p.Id == productId
            );

            if (product == null)
            {
                return Result<ProductResponse>.Fail("Product not found.");
            }

            productRequest.Adapt(product);

            if (productRequest.MainImage != null)
            {
                var imageResult = await _fileService.UploadFileAsync(
                    productRequest.MainImage
                );

                if (!imageResult.IsSuccess || imageResult.Data == null)
                {
                    return Result<ProductResponse>.Fail($"Main image upload failed: {imageResult.Message}");
                }

                product.MainImage = imageResult.Data;
            }
            if (productRequest.SubImages != null)
            {
                var subImageNames = new List<string>();

                foreach (var image in productRequest.SubImages)
                {
                    var result = await _fileService.UploadFileAsync(image);

                    if (!result.IsSuccess || result.Data == null)
                    {
                        return Result<ProductResponse>.Fail($"Sub image upload failed: {result.Message}");
                    }

                    subImageNames.Add(result.Data);
                }

                product.SubImages = subImageNames;
            }

            await _productRepository.UpdateAsync(product);

            return Result<ProductResponse>.Ok(product.Adapt<ProductResponse>(), "Product updated successfully.");

        }
    }
}