using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.DAL.Models
{
    public class ProductTranslation
    {
        public int Id { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}