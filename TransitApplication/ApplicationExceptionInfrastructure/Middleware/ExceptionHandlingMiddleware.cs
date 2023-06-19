using TransitApplication.BaseExceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace TransitApplication.Middlewares
{
    internal class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ExceptionBase ex)
            {
                await HandleException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleUnexpectedException(context, ex);
            }
        }

        public Task HandleException(HttpContext context, ExceptionBase ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

            string result = JsonSerializer.Serialize(new 
            { 
                message = ex.Message, 
                description = ex.Description,
                requestId = context.TraceIdentifier
            });

            return context.Response.WriteAsync(result);
        }

        public Task HandleUnexpectedException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

            string result = JsonSerializer.Serialize(new
            {
                message = "Internal server error: unexpected exception thrown",
                description = $"{ex.GetType().Name}: {ex.Message}",
                requestId = context.TraceIdentifier
            });

            return context.Response.WriteAsync(result);
        }
    }
}
