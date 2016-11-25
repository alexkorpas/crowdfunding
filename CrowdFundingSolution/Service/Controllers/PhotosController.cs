using BAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Service
{
    public class PhotosController : ApiController
    {        
        /// <summary>
        /// Insert or update Images
        /// </summary>
        /// <param name="jobj"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveImage(JObject jobj)
        {
            if (jobj == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Post data is null");
            try
            {
                using (var repository = new CrowdFundingTransactions())
                {
                    var user = User.Identity.Name;
                    var transaction = await repository.SaveImageTransaction(jobj.ToObject<ProjectPhotoDTO>(), user);
                    if (transaction.Result == TransResult.Success)
                        return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
                }
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteImage(int Id)
        {
            try
            {
                using (var repository = new CrowdFundingTransactions())
                {
                    var user = User.Identity.Name;
                    var transaction = await repository.DeleteImageTransaction(Id, user);
                    if (transaction.Result == TransResult.Success)
                        return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
                }
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> SetMainPhoto(int Id)
        {
            try
            {
                using (var repository = new CrowdFundingTransactions())
                {
                    var user = User.Identity.Name;
                    var transaction = await repository.SetMainPhotoTransaction(Id, user);
                    if (transaction.Result == TransResult.Success)
                        return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
                }
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectImages(int Id)
        {
            try
            {
                using (var repository = new CrowdFundingTransactions())
                {
                    var transaction = await repository.ReadProjectImages(Id);
                    if (transaction.Result == TransResult.Success)
                        return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
                }
            }
            catch (Exception ex) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message); }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetProjectMainImage(int id, int? pointer = null)
        {
            try
            {
                using (var repository = new CrowdFundingTransactions())
                {
                    var transaction = await repository.ReadProjectMainImage(id, pointer);
                    if (transaction.Result == TransResult.Success)
                        return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, transaction.Message);
                }
            }
            catch (Exception ex) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message); }
        }
    }
}