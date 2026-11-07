using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.DAL.Models
{
    public class Product : AuditableEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string MainImage { get; set; }
        public bool IsActive { get; set; }
        public string? SubImages { get; set; }
        public double Rate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<ProductTranslation> Translations { get; set; }
            = new List<ProductTranslation>();
    }
}