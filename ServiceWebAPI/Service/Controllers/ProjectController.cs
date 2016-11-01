using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service
{
    public class ProjectController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetProjects()
        {
            try
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "success");
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }
    }
}
