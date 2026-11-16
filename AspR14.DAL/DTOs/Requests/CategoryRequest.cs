using System;
using System.Collections.Generic;
using System.Text;

namespace AspR14.DAL.DTOs.Requests
{
    public class CategoryRequest
    {
        public List<CategoryTranslationRequest> Translations { get; set; } = new();
    }
}
