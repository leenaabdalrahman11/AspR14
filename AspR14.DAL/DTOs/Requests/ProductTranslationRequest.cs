using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.DAL.DTOs.Requests
{
    public class ProductTranslationRequest
    {
        public string Language { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}