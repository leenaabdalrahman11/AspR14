using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.DAL.DTOs.Requests
{
    public class CartItemRequest
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}