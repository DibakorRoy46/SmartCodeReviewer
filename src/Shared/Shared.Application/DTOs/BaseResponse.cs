
namespace Shared.Application.DTOs;

public class BaseResponse<T>
{
    public bool Success { get; }
    public string? ErrorMessage { get; }
    public T? Data { get; }

    public BaseResponse(T data)
    {
        Success = true;
        Data = data;
    }

    public BaseResponse(string errorMessage)
    {
        Success = false;
        ErrorMessage = errorMessage;
    }
}
