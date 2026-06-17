using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace KrediHesaplama.Api.Infrastructure;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Loglama Seviyesi Kontrolü: Debug seviyesi açıksa tüm detayları (stacktrace vb.) göster.
        // Kapalıysa sadece "tatlı" kısa mesajı göster.
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogError(exception, "Detaylı Hata Kaydı: {Message}", exception.Message);
        }
        else
        {
            _logger.LogError("Hata Oluştu: {Message}", exception.Message);
        }

        var problemDetails = new ProblemDetails
        {
            Status = exception switch
            {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            },
            Title = "Bir hata oluştu",
            Detail = _env.IsDevelopment() ? exception.ToString() : exception.Message,
            Instance = httpContext.Request.Path
        };

        if (exception is BadHttpRequestException)
        {
            problemDetails.Title = "Geçersiz İstek";
            problemDetails.Detail = "Gönderilen veri formatı hatalı. Lütfen JSON formatını kontrol edin.";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
