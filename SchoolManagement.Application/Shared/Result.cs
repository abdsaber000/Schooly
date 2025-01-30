using System.Net;

namespace SchoolManagement.Application.Shared;

public class Result<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public static Result<T> Success(T Data)
    {
        return new Result<T>()
        {
            Data = Data,
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };
    }
    public static Result<string> SuccessMessage(string message)
    {
        return new Result<string>()
        {
            Message = message,
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK
        };
    }
    public static Result<T> Success(T Data, string Token)
    {
        return new Result<T>()
        {
            Data = Data,
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Token = Token
        };
    }
    public static Result<T> Success(string Token)
    {
        return new Result<T>()
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Token = Token
        };
    }
    public static Result<T>Failure(string message)
    {
        return new Result<T>()
        {
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };
    }
}