using Dsw2026Ej15.Dsw2026Ej15.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Dsw2026Ej15.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation error",
                    Detail = ex.Message
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Problem",
                    Detail = "Unexpected error"
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
