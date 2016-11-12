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

        [HttpGet]
        public HttpResponseMessage GetProjectsByUser(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = repository.ReadProjectsByUserId(3);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public HttpResponseMessage GetProjectCategories()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = repository.ReadProjectCategories();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public HttpResponseMessage GetProjectByCategory(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = repository.ReadProjectByCategory(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public HttpResponseMessage GetProjectByState(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = repository.ReadProjectByState(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public HttpResponseMessage GetProjectStates()
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = repository.ReadProjectStates();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public HttpResponseMessage GetProjectPhotoById(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var result = repository.ReadProjectPhotoById(id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }
    }
}
