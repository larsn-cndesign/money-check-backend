using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Models.Localization;
using System.Collections;
using System.Net;

namespace MoneyCheck.Api.Middleware
{
  public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
  {
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;

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

    private Task ConvertException(HttpContext context, Exception exception)
    {
      HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

      context.Response.ContentType = "application/json";

      string? result;
      string extraData = string.Empty;

      switch (exception)
      {
        case ValidationException validationException:
          httpStatusCode = HttpStatusCode.BadRequest;
          result = validationException.ValdationError;
          break;

        case BadRequestException badRequestException:
          httpStatusCode = HttpStatusCode.BadRequest;
          result = badRequestException.Message;
          break;

        case NotFoundException notFoundException:
          httpStatusCode = HttpStatusCode.NotFound;
          if (exception.Data.Count > 0)
          {
            var firstEntry = exception.Data.Cast<DictionaryEntry>().FirstOrDefault();
            extraData = $"{firstEntry.Key} {firstEntry.Value}";
          }
          result = new LocaleError(LocaleErrorKey.NotFound, notFoundException.Message).ToJson();
          break;

        case UnauthorizedAccessException:
          httpStatusCode = HttpStatusCode.Unauthorized;
          result = new LocaleError(LocaleErrorKey.Unauthorized).ToJson();
          break;

        case Exception:
          httpStatusCode = HttpStatusCode.BadRequest;
          result = new LocaleError(LocaleErrorKey.Unknown).ToJson();
          break;

        default:
          result = new LocaleError(LocaleErrorKey.UnknownServerError).ToJson();
          break;
      }

      context.Response.StatusCode = (int)httpStatusCode;

      _logger.LogError("Error Message: {Message}", exception.Message);
      _logger.LogError("Error Result: {Result}", result);
      _logger.LogError("Error Result info: {Data}", extraData);
      _logger.LogError("Error Inner Exception: {Data}", exception.InnerException);
      _logger.LogError("Error StackTrace: {StackTrace}", exception.StackTrace);

      return context.Response.WriteAsync(result);
    }
  }
}