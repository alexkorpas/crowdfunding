using BAL;
using DAL;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Data.Common;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Service
{
    [Authorize]
    public class PaymentController : ApiController
    {
        private Guid _MerchantId = new Guid("5964e387-437d-4e70-a795-6e346f92b57a");
        private string _ApiKey = "@MTv$s";
        private string _BaseApiUrl = "http://demo.vivapayments.com";
        private string _PaymentsUrl = "/api/transactions";
        private string _PaymentsCreateOrderUrl = "/api/orders";

        [HttpPost]
        public HttpResponseMessage Pay(string ourToken, int amountPledged, int projectId)
        {
            var context = new backup_CrowdFundingViva1Entities();
            using (var dbTran = context.Database.BeginTransaction())
            {
                try
                {

                    var payment = new Payment();
                    payment.Amount = amountPledged;
                    payment.PaymentDate = DateTime.Now;
                    payment.ProjectFK = projectId;
                    payment.PaymentMethod = "";

                    context.Payment.Add(payment);

                    var project = context.Project.Find(projectId);
                    project.GoalMin = project.GoalMin == null ? (decimal)amountPledged : project.GoalMin+ (decimal)amountPledged;

                    context.SaveChanges();

                var _orderCode = CreateOrder(amountPledged);    //CALL TO CREATE AN ORDER. IF AN ORDER CODE ALREADY EXISTS FROM A PREVIOUS STEP, USE THAT ONE INSTEAD

                var cl = new RestClient(_BaseApiUrl);
                cl.Authenticator = new HttpBasicAuthenticator(
                                        _MerchantId.ToString(),
                                        _ApiKey);

                var req = new RestRequest(_PaymentsUrl, Method.POST);
                req.RequestFormat = DataFormat.Json;
                req.AddBody(new
                {
                    OrderCode = _orderCode,
                    SourceCode = "Default",             //MAKE SURE THIS IS A SOURCE OF TYPE SIMPLE/NATIVE  
                    CreditCard = new
                    {
                        Token = ourToken//.Value.ToString()
                    }
                });

               
                var res = cl.Execute<TransactionResult>(req);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (res.Data.ErrorCode == 0 && res.Data.StatusId == "F")
                    {

                            //Response.Write(string.Format(
                            //    "Transaction was successful. TransactionId is {0}",
                            //    res.Data.TransactionId));
                            //payment.tra
                            //save
                            payment.PaymentMethod = res.Data.TransactionId.ToString();
                            context.SaveChanges();
                            dbTran.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, string.Format("Transaction was successful. TransactionId is {0}", res.Data.TransactionId));
                    }
                    else
                    {
                            //Response.Write(string.Format(
                            //    "Transaction failed. Error code was {0}",
                            //    res.Data.ErrorCode));
                            dbTran.Rollback();
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Format("Transaction failed. Error code was {0}", res.Data.ErrorCode));
                    }
                }
                else
                {
                        //Response.Write(string.Format(
                        //    "Transaction failed. Error code was {0}",
                        //    res.StatusCode));
                        dbTran.Rollback();
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Format("Transaction failed. Error code was {0}", res.StatusCode));
                }
                }
                catch (Exception)
                {
                    dbTran.Rollback();
                    throw;
                }
            }
        }

        private long CreateOrder(long amount)
        {
            var cl = new RestClient(_BaseApiUrl);
            cl.Authenticator = new HttpBasicAuthenticator(
                                    _MerchantId.ToString(),
                                    _ApiKey);

            var req = new RestRequest(_PaymentsCreateOrderUrl, Method.POST);

            req.AddObject(new
            {
                Amount = amount * 100,    // Amount is in cents
                SourceCode = "Default"
            });

            var res = cl.Execute<OrderResult>(req);

            if (res.Data != null && res.Data.ErrorCode == 0)
            {
                return res.Data.OrderCode;
            }
            else
                return 0;
        }

        public class OrderResult
        {
            public long OrderCode { get; set; }
            public int ErrorCode { get; set; }
            public string ErrorText { get; set; }
            public DateTime TimeStamp { get; set; }

        }

        public class TransactionResult
        {
            public string StatusId { get; set; }
            public Guid TransactionId { get; set; }
            public int ErrorCode { get; set; }
            public string ErrorText { get; set; }
            public DateTime TimeStamp { get; set; }
        }

        public bool TestConnection()
        {
            using (var db = new backup_CrowdFundingViva1Entities())
            {
                DbConnection conn = db.Database.Connection;
                try
                {
                    conn.Open();   // check the database connection
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        [HttpGet]
        [Route("GetPayments")]
        public async Task<HttpResponseMessage> GetPayments([FromUri]TransactionCriteria criteria)
        {
            try
            {
                using (var repo = new CrowdFundingTransactions())
                {
                    var transaction = await repo.ReadPayments(criteria);
                    if (transaction.Result == TransResult.Success)
                        return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, transaction.Message);
                }

            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }
    }
}