using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Project.Manager.Api.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Description = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}