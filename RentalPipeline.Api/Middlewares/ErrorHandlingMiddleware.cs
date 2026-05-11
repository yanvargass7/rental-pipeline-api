using System.Net;
using System.Text.Json;
using RentalPipeline.Application.DTOs;
using RentalPipeline.Domain.Exceptions;

namespace RentalPipeline.API.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (Exception)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Internal server error");
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        var json =JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}