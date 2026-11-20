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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public ProductService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Result<ProductResponse>> CreateProductAsync(
    ProductRequest productRequest)
        {
            if (productRequest.MainImage == null)
            {
                return Result<ProductResponse>.Fail(
                    "Main image is required for product creation.");
            }

            var mainImageUploaded = await _fileService.UploadFileAsync(productRequest.MainImage);

            if (!mainImageUploaded.IsSuccess || mainImageUploaded.Data == null)
            {
                return Result<ProductResponse>.Fail(
                    $"Main image upload failed: {mainImageUploaded.Message}");
            }

            var subImageDatas = new List<FileResult>();

            if (productRequest.SubImages != null)
            {
                foreach (var image in productRequest.SubImages)
                {
                    var result = await _fileService.UploadFileAsync(image);

                    if (!result.IsSuccess || result.Data == null)
                    {
                        return Result<ProductResponse>.Fail(
                            $"Sub image upload failed: {result.Message}");
                    }

                    subImageDatas.Add(result.Data);
                }
            }

            var product = productRequest.Adapt<Product>();

            product.MainImage = mainImageUploaded.Data.FileUrl;//To Access the URL of the uploaded main image in Cloudinary for display the image in the application or for any other purpose where the image URL is needed.
            product.MainImageUploadId = mainImageUploaded.Data.PublicId;//To Access the public ID of the uploaded main image in Cloudinary for future reference or deletion.

            product.SubImages = subImageDatas.Select(d => d.FileUrl).ToList();
            product.SubImageUploadIds = subImageDatas.Select(d => d.PublicId).ToList();

            
            await _unitOfWork.ProductRepository.CreateAsync(product);
            await _unitOfWork.CompleteAsync();

            return Result<ProductResponse>.Ok(product.Adapt<ProductResponse>(), "Product created successfully.");
        }

        public async Task<Result<bool>> DeleteProductAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await _unitOfWork.ProductRepository.GetOne(predicate);
            if (product == null)
            {
                return Result<bool>.Fail("Product not found.");
            }
            await _fileService.DeleteAsync(product.MainImageUploadId);
            if (product.SubImageUploadIds != null)
            {
                foreach (var subImageId in product.SubImageUploadIds)
                {
                    await _fileService.DeleteAsync(subImageId);
                }
            }

            _unitOfWork.ProductRepository.Delete(product);
            var completed = await _unitOfWork.CompleteAsync();
            return Result<bool>.Ok(completed > 0, completed > 0 ? "Product deleted successfully." : "Failed to delete product.");
        }

        public async Task<Result<List<ProductResponse>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync(new string[]
            {
                    nameof(Product.Category),
                    nameof(Product.Translations)
            });
            return Result<List<ProductResponse>>.Ok(products.Adapt<List<ProductResponse>>(), "Products retrieved successfully.");

        }

        public async Task<Result<ProductResponse>> GetProductByIdAsync(Expression<Func<Product, bool>> filter)
        {
            var product = await _unitOfWork.ProductRepository.GetOne(filter, new string[]
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
            var product = await _unitOfWork.ProductRepository.GetOne(
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

                product.MainImage = imageResult.Data.FileUrl;
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

                    subImageNames.Add(result.Data.FileUrl);
                }

                product.SubImages = subImageNames;
            }

            await _unitOfWork.ProductRepository.UpdateAsync(product);

            return Result<ProductResponse>.Ok(product.Adapt<ProductResponse>(), "Product updated successfully.");

        }
    }
}