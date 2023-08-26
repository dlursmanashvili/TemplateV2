using System.Net;
using System.Text.Json;

namespace Template.Api.Middlewares;

public class ExceptionMiddleware
{
    #region Constructor

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    #endregion


    /// <summary>
    /// After getting the request, if Exception is thrown, calls <see cref="HandleExceptionAsync"/> method
    /// </summary>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// After caching exception, checks what type of exception is thrown.
    /// </summary>
    /// <returns>Json serialized exception result with StatusCode </returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string message = "";

        var exceptionType = exception.GetType();


        switch (exceptionType.Name)
        {
            case "BadRequestException":
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                break;
            case "NotFoundException":
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                break;
            case "NotFoundKeyException":
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                break;
            case "NotImplementedException":
                message = exception.Message;
                status = HttpStatusCode.NotImplemented;
                break;
            case "UnAuthorizedException":
                message = exception.Message;
                status = HttpStatusCode.Unauthorized;
                break;
            default:
                message = exception.Message;
                status = HttpStatusCode.InternalServerError;
                break;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }
}
