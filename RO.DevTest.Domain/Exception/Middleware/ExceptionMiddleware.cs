using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RO.DevTest.Domain.Exception.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (System.Exception ex)
        {
            _logger.LogError(ex, $"An error occurred: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode = (int)HttpStatusCode.InternalServerError;
        object response;

        switch (exception)
        {
            case ApiException apiException:
                statusCode = (int)apiException.StatusCode;
                response = new
                {
                    StatusCode = statusCode,
                    message = (int)apiException.StatusCode,
                    Errors = apiException.Errors
                };
                break;

            case ValidationException validationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                response = new
                {
                    StatusCode = statusCode,
                    message = (int)HttpStatusCode.BadRequest,
                    Errors = validationException.ValidationResult.ErrorMessage
                };
                break;

            default:
                response = new
                {
                    StatusCode = statusCode,
                    message = (int)HttpStatusCode.InternalServerError,
                    Errors = new[] {exception.Message}
                };
                break;
        }
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
