using System.Net;
using System.Text.Json;
using ErrorResultModel = Roof.ClientAPI.Exceptions.Model.ErrorResultModel;
using ForbiddenException = Roof.ClientAPI.Exceptions.ForbiddenException;
using UnauthorizedException = Roof.ClientAPI.Exceptions.UnauthorizedException;

namespace Roof.ClientAPI.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private static Task ConvertException(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode;

        context.Response.ContentType = "application/json";
        string message;
        switch (exception)
        {
            case ForbiddenException forbiddenException:
                httpStatusCode = HttpStatusCode.Forbidden;
                message = forbiddenException.Message;
                break;
            case UnauthorizedException unauthorizedException:
                httpStatusCode = HttpStatusCode.Forbidden;
                message = unauthorizedException.Message;
                break;
            default:
                httpStatusCode = HttpStatusCode.InternalServerError;
                message = exception.Message;
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;
        var errorModel = new ErrorResultModel((int)httpStatusCode, message);
        return context.Response.WriteAsync(JsonSerializer.Serialize(errorModel));
    }
}
