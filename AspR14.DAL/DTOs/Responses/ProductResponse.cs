using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.Models;

namespace AspR14.DAL.DTOs.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
           public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string MainImage { get; set; }
        public bool IsActive { get; set; }
        public List<string> SubImages { get; set; } = new();
        public double Rate { get; set; }
        public int CategoryId { get; set; }
        
    }
}