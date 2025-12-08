using ChatAppSlnVersionII.Shared.ApiResponses;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Net;
using System.Text.Json;
using static ChatAppSlnVersionII.Shared.ExceptionHandler.ExceHandlers;

namespace ChatAppSlnVersionII.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (PostgresException sqlEx)
            {
                await HandleExceptionAsync(context, sqlEx);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            return ex switch
            {
                ValidationExceptionBase customValidationEx => HandleCustomValidationExceptionAsync(context, customValidationEx),
                NoContentException ncEx => HandleSimpleException(context, ncEx, HttpStatusCode.NoContent),
                BadRequestException badEx => HandleSimpleException(context, badEx, HttpStatusCode.BadRequest),
                PostgresException pex => HandleSimpleException(context, pex, HttpStatusCode.InternalServerError),
                _ => HandleSimpleException(context, ex, HttpStatusCode.InternalServerError, "Something went wrong.")
            };
        }

        private Task HandleCustomValidationExceptionAsync(HttpContext context, ValidationExceptionBase ex)
        {
            context.Response.StatusCode = 200;

            var errorList = ex.Errors
                .Select(kvp => new FieldError
                {
                    Field = kvp.Key,
                    Messages = kvp.Value
                })
                .ToList();

            var response = new ApiExceptionResponse<FieldError>
            {
                ResultType = ResultType.ValidationException,
                Message = "Validation Error",
                Error = ex.InnerException?.Message ?? ex.Message,
                Errors = errorList
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private Task HandleSimpleException(HttpContext context, Exception ex, HttpStatusCode statusCode, string defaultMessage = null)
        {
            context.Response.StatusCode =200;

            if (statusCode == HttpStatusCode.NoContent)
                return Task.CompletedTask;

            var response = new ApiExceptionResponse
            {
                ResultType = ResultType.Error,
                Message = defaultMessage ?? "Error occurred",
                Error = ex.InnerException?.Message ?? ex.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
