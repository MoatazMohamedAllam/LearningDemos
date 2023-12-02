using ExceptionHandling_Middlewares_vs_Filters.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExceptionHandling_Middlewares_vs_Filters.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new Error
            {
                StatusCode = "500",
                Message = context.Exception.Message,
            };

            context.Result = new JsonResult(error) { StatusCode = 500 };
        }
    }
}
