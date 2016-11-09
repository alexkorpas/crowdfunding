using BAL;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service
{
    public class ProjectController : ApiController
    {
        /// <summary>
        /// Gets a list of all available Projects
        /// </summary>
        /// <returns>List<ProjectDTO></returns>
        [HttpGet]
        public HttpResponseMessage GetProjects(int ?id=null)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = repository.ReadProjects(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }
    }
}
