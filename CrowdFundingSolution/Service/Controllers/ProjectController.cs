using BAL;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Service
{
    public class ProjectController : ApiController
    {
        /// <summary>Gets a list of all available Projects</summary>
        /// <returns>List<ProjectDTO></returns>
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
        public async Task<HttpResponseMessage> GetProjectsByPage(int page)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = await repository.ReadProjectsByPage(page);
                
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        ///<summary>Gets the Number of Projects in database so that client can start pagination .</summary>
        ///<returns>Project Ammount</returns>

        [HttpGet]
        public async Task<HttpResponseMessage> ReadAndCount()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = await repository.ReadAndCount();
                var json = new
                {
                    Table_Size = result
                    
                };
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        ///<summary>Gets the Number of Projects in database so that client can start pagination .</summary>
        ///<returns>Project Ammount</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectsByUser(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result =await repository.ReadProjectsByUserId(id);
        ///<summary>Gets a list of all available Projects a user has created, given his id.</summary>
        ///<returns>List<ProjectDTO></returns>
        //[HttpGet]
        //public async Task<HttpResponseMessage> GetProjectsByUser(int id)
        //{
        //    try
        //    {
        //        var repository = new CrowdFundingTransactions();
        //        var result =await repository.ReadProjectsByUserId(id);

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        //}

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

        //[HttpGet]
        //public async Task<HttpResponseMessage> GetProjectByCategory(int id)
        //{
        //    try
        //    {
        //        var repository = new CrowdFundingTransactions();
        //        var result = await repository.ReadProjectByCategory(id);

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        //}

        //[HttpGet]
        //public async Task<HttpResponseMessage> GetProjectByState(int id)
        //{
        //    try
        //    {
        //        var repository = new CrowdFundingTransactions();
        //        var result = await repository.ReadProjectByState(id);

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        //}

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

        ///<summary>Gets a list of all available Projects that match a keyword-keyphrase.</summary>
        ///<returns>List<ProjectDTO></returns>
        //[HttpGet]
        //public async Task<HttpResponseMessage> GetSearchProjects(string keyword)
        //{
        //    try
        //    {
        //        var repository = new CrowdFundingTransactions();
        //        var result = await repository.SearchProjectsByKeyword(keyword);

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        //}
    }
}
