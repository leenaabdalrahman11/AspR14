using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.DAL.DTOs.Responses
{
    public class RegisterResponse
    {
        public string Message {get;set;}
        public List<string> Errors {get;set;}
        
    }
}