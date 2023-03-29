using FluentValidation;
using Services.Exceptions;
using System.Net.NetworkInformation;

namespace WebShop.MiddleWare
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //request logic (/)
            try
            {
                //next
                await _next.Invoke(context);
            }
            catch(NotAuthorizedException ex)
            {
                await WriteReposneAsync(context, 403, ex.Message);
            }
            catch(ValidationException ex)
            {
                await WriteReposneAsync(context, 400, ex.Message);
            }
            catch(ResourceNotFoundException ex)
            {
                await WriteReposneAsync(context, 204, ex.Message);
            }
            catch(Exception ex)
            {
                // response logic
                await WriteReposneAsync(context, 500, ex.Message);
            }
        }


        private static async Task WriteReposneAsync(HttpContext context, int status, string message)
        {
            context.Response.StatusCode = status;
            await context.Response.WriteAsync(message);

        }

    }
}
