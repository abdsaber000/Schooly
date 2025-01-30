using System.Net;

namespace SchoolManagement.Application.Shared;

public class Result<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public static Result<T> Success(T Data , HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Result<T>()
        {
            Data = Data,
            IsSuccess = true,
            StatusCode = statusCode
        };
    }
    
    public static Result<string> SuccessMessage(string message , HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Result<string>()
        {
            Message = message,
            IsSuccess = true,
            StatusCode = statusCode
        };
    }
    
    public static Result<T> Success(T Data, string Token , HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Result<T>()
        {
            Data = Data,
            IsSuccess = true,
            StatusCode = statusCode,
            Token = Token
        };
    }
    public static Result<T> Success(string message, string Token, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Result<T>()
        {
            Message = message,
            IsSuccess = true,
            StatusCode = statusCode,
            Token = Token
        };
    }
    public static Result<T>Failure(string message , HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Result<T>()
        {
            Message = message,
            StatusCode = statusCode
        };
    }
}