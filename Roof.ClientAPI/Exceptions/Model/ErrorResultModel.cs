namespace Roof.ClientAPI.Exceptions.Model;

public class ErrorResultModel
{
    public int StatusCode { get; }
    public string Message { get; }

    public ErrorResultModel(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}