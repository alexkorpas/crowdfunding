using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Service.Services
{
    public class CFExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is Exception)
            {
                actionExecutedContext.Response = new HttpResponseMessage()
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message, System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}