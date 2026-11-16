using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace AspR14.DAL.DTOs.Requests
{
    public class ProductRequest
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public IFormFile MainImage { get; set; }
        public bool IsActive { get; set; }
        public List<IFormFile> SubImages { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<ProductTranslation> Translations { get; set; }
            = new List<ProductTranslation>();
    }
}