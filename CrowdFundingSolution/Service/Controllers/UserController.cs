using BAL;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using DAL;
using BAL.DTO;

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
        public async Task<HttpResponseMessage> GetLoggedInUser()
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

        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateUser(JObject jobj)
        {
            if (jobj == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Post data is null");
            try
            {
                var user = User.Identity.Name;
                AspNetUsersDTO userInfoDTO = jobj.ToObject<AspNetUsersDTO>();
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.SaveUserTransaction(userInfoDTO, user);
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Id);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }
    }
}