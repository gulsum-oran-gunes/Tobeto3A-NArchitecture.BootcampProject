using System.Linq.Expressions;
using System.Net.Mime;
using System.Text.Json;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.CrossCuttingConcerns.Exception.WebApi.Handlers;
using NArchitecture.Core.CrossCuttingConcerns.Logging;

namespace WebApi.Exception;

public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly NArchitecture.Core.CrossCuttingConcerns.Logging.Abstraction.ILogger _loggerService;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, NArchitecture.Core.CrossCuttingConcerns.Logging.Abstraction.ILogger loggerService)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _loggerService = loggerService;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception exception)
        {
            Console.WriteLine("Invoke");
            Console.WriteLine(exception.GetType().Name);
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private Task HandleExceptionAsync(HttpResponse response, System.Exception exception)
    {
        response.ContentType = MediaTypeNames.Application.Json;
        _httpExceptionHandler.Response = response;
        Console.WriteLine("HandleExceptionAsync");
        Console.WriteLine(exception.GetType().Name);
        return exception switch
        {
            BusinessException businessException => _httpExceptionHandler.HandleException(businessException),
            ValidationException validationException => _httpExceptionHandler.HandleException(validationException),
            AuthorizationException authorizationException => _httpExceptionHandler.HandleException(authorizationException),
            NotFoundException notFoundException => _httpExceptionHandler.HandleException(notFoundException),
            _ => _httpExceptionHandler.HandleException(exception)
        };
    }

    private Task LogException(HttpContext context, System.Exception exception)
    {
        List<LogParameter> logParameters = [new LogParameter { Type = context.GetType().Name, Value = exception.ToString() }];

        LogDetail logDetail =
            new()
            {
                MethodName = _next.Method.Name,
                Parameters = logParameters,
                User = _contextAccessor.HttpContext?.User.Identity?.Name ?? "?"
            };

        _loggerService.Information(JsonSerializer.Serialize(logDetail));
        return Task.CompletedTask;
    }
}