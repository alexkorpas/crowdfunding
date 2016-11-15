using BAL;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Service
{
    public class ProjectDetailsController : ApiController
    {
        ///<summary>Gets the photo by photo id.</summary>
        ///<returns>List<ProjectPhotoDTO></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectPhotoById(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = await repository.ReadProjectPhotoById(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }


    }
}