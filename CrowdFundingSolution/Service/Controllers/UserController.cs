using BAL;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Service.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        /// <summary>
        /// Returns a user dto of the currently logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetLoggedInUser()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var identity = User.Identity as ClaimsIdentity;
                var result = repository.ReadUserByName(identity.Name);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }
    }
}