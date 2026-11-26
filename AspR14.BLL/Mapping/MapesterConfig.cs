using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Migrations;
using AspR14.DAL.Models;
using Mapster;

namespace AspR14.BLL.Mapping
{
    public class MapesterConfig
    {
        public static void MapsterConfigRegister() // static bc we want to call it without creating an instance of the class
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
            .Map(dest => dest.Name, src => src.Translations.Where(t => t.Language == CultureInfo.CurrentUICulture.Name)
            .Select(t => t.Name).FirstOrDefault());

            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
            .Map(dest => dest.Name, src => src.Translations.Where(t => t.Language == CultureInfo.CurrentUICulture.Name)
            .Select(t => t.Name).FirstOrDefault())
            .Map(dest => dest.Description, src => src.Translations.Where(t => t.Language == CultureInfo.CurrentUICulture.Name)
            .Select(t => t.Description).FirstOrDefault())
            .Map(dest => dest.Language,
     src => src.Translations
         .Where(t => t.Language == CultureInfo.CurrentUICulture.Name)
         .Select(t => t.Language)
         .FirstOrDefault());
       
            TypeAdapterConfig<ProductRequest, Product>
    .NewConfig()
    .Ignore(dest => dest.MainImage)
    .Ignore(dest => dest.SubImages);
        

          TypeAdapterConfig<CartItem, CartItemResponse>.NewConfig()
            .Map(dest => dest.ProductName, src => src.Product.Translations.Where(t => t.Language == CultureInfo.CurrentUICulture.Name)
            .Select(t => t.Name).FirstOrDefault())
            .Map(dest => dest.Price, src => src.Product.Price)
            .Map(dest => dest.MainImageUrl,src => src.Product.MainImage);
    }}
}