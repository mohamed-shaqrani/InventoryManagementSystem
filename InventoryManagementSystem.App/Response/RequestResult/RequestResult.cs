namespace InventoryManagementSystem.App.Response.RequestResult;

public record RequestResult<T>(T Data, bool IsSuccess, string Message, ErrorCode ErrorCode)
{
    public static RequestResult<T> Success(T data, string message)
    {

        return new RequestResult<T>(data, true, message, ErrorCode.None);
    }
    public static RequestResult<T> Failure(ErrorCode errorCode, string message, T Date = default)
    {

        return new RequestResult<T>(Date, false, message, errorCode);
    }
}

