using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.DAL.DTOs.Responses
{
    
public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static Result<T> Ok(T? data, string message = "Success")
    {
        return new Result<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

    public static Result<T> Fail(string message)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message
        };
    }

    public static Result<T> Fail(string message, List<string> errors)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}

}