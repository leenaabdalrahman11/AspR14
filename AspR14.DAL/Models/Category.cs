using System;
using System.Collections.Generic;
using System.Text;

namespace AspR14.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public ICollection<CategoryTranslation> Translations { get; set; }
            = new List<CategoryTranslation>();

    }
}