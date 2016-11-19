using BAL;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Service
{
    public class ProjectController : ApiController
    {
        [HttpGet]
        [Route("GetProjects")]
        public async Task<HttpResponseMessage> GetProjects([FromUri]TransactionCriteria criteria)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.ReadProjects(criteria);
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetPageCount(string keyword)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.ReadPageCount(keyword);
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectCategories()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.ReadProjectCategories();
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectStates()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.ReadProjectStates();
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        /////<summary>Gets the photo by photo id.</summary>
        /////<returns>List<ProjectPhotoDTO></returns>
        //[HttpGet]
        //public async Task<HttpResponseMessage> GetProjectPhotoById(int id)
        //{
        //    try
        //    {
        //        var repository = new CrowdFundingTransactions();
        //        var result = await repository.ReadProjectPhotoById(id);

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        //}

        [HttpGet]
        public async Task<HttpResponseMessage> GetTrendingProjects()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.ReadTrendingProjects();
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }        
    }
}
