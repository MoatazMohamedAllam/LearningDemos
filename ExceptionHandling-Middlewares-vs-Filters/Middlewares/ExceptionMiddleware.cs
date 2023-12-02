using ExceptionHandling_Middlewares_vs_Filters.Contracts;
using System.Net;

namespace ExceptionHandling_Middlewares_vs_Filters.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new Error
                {
                    Message = ex.Message,
                    StatusCode = context.Response.StatusCode.ToString(),
                };
                await context.Response.WriteAsync(error.ToString());
            }
        }
    }
}
