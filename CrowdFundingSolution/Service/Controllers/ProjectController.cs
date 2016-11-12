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
        /// <summary>
        /// Gets a list of all available Projects
        /// </summary>
        /// <returns>List<ProjectDTO></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetProjects(int ?id=null)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = await repository.ReadProjects();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectsByUser(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result =await repository.ReadProjectsByUserId(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectCategories()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result =await repository.ReadProjectCategories();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectByCategory(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = await repository.ReadProjectByCategory(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectByState(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = await repository.ReadProjectByState(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectStates()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = await repository.ReadProjectStates();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

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
