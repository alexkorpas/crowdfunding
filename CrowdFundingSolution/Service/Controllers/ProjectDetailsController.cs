﻿using BAL;
using Newtonsoft.Json.Linq;
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

        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveProjectFunding(JObject jobj)
        {
            if (jobj == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Post data is null");
            try
            {
                var user = User.Identity.Name;
                ProjectFundingLevelDTO projectFundingLevelDTO = jobj.ToObject<ProjectFundingLevelDTO>();
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.SaveProjectFundingTransaction(projectFundingLevelDTO, user);
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Id);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectFundingLevels(int id)
        {
            try
            {
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.ReadProjectFundingLevels(id);
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

    }
}