using System;
using System.Collections.Generic;
using System.Text;

namespace AspR14.DAL.Models
{
    public class CategoryTranslation
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Language { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public Category Category { get; set; } = null!;
    }
}
