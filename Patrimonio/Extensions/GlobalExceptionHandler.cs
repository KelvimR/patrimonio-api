using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Patrimonio.Exceptions;

namespace Patrimonio.Extensions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        //Cancelamento causado pelo cliente
        if (exception is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested)
        {
            _logger.LogWarning("Requisição cancelada pelo cliente. TraceId: {TraceId}", httpContext.TraceIdentifier);

            //Não tenta enviar uma resposta, pois o cliente já cancelou a requisição
            return true;
        }

        var statusCode = exception switch
        {
            ConflictException => StatusCodes.Status409Conflict,
            BusinessException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,            
            _ => StatusCodes.Status500InternalServerError
        };

        if(statusCode >= 500)
        {
            _logger.LogError(exception, "Erro inesperado. TraceId: {TraceId}", httpContext.TraceIdentifier);
        }
        else
        {
            _logger.LogWarning(exception, "Erro na requisição. TraceId: {TraceId}", httpContext.TraceIdentifier);
        }

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode switch
            {
                400 => "Erro de negócio",
                404 => "Recurso não encontrado",
                403 => "Acesso não autorizado",
                _ => "Erro interno do servidor"
            },
            Detail = statusCode >= 500 ? "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde." : exception.Message,
            Instance = httpContext.Request.Path
        };

        problem.Extensions["traceId"] = httpContext.TraceIdentifier;
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;

    }
}
