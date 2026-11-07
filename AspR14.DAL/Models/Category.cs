using System;
using System.Collections.Generic;
using System.Text;
namespace AspR14.DAL.Models
{
    public class Category : AuditableEntity
    {
        public List<CategoryTranslation> Translations { get; set; } = new();
        public List<Product> Products { get; set; } = new();
    }
}