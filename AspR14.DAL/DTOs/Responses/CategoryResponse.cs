using System;
using System.Collections.Generic;
using System.Text;
using AspR14.DAL.Models;

namespace AspR14.DAL.DTOs.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
