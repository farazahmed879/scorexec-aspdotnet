using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ScoreExec.Application.Common.Exceptions;

namespace ScoreExec.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var problemDetails = exception switch
        {
            ValidationException validationEx => new ValidationProblemDetails(validationEx.Errors)
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Validation Failed",
                Detail = "One or more validation errors occurred."
            },
            NotFoundException notFoundEx => new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Not Found",
                Detail = notFoundEx.Message
            },
            UnauthorizedException unauthorizedEx => new ProblemDetails
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = "Unauthorized",
                Detail = unauthorizedEx.Message
            },
            _ => new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred."
            }
        };

        context.Response.StatusCode = problemDetails.Status ?? 500;

        var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
