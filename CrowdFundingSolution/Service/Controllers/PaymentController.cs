﻿using BAL;
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
        public HttpResponseMessage Pay(string ourToken, int amountPledged)
        {
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

            //paymentform.Visible = false;
            //using (var db = new backup_CrowdFundingViva1Entities())
            //{
            //    DbConnection conn = db.Database.Connection;
            //    try
            //    {
            //        conn.Open();   // check the database connection
            //        var res = cl.Execute<TransactionResult>(req);
            //        db.Payment.Add(new Payment( ));
            //    }
            //    catch
            //    {
            //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Connection Lost");
            //    }
            //}
            var res = cl.Execute<TransactionResult>(req);
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (res.Data.ErrorCode == 0 && res.Data.StatusId == "F")
                {
                    
                    //Response.Write(string.Format(
                    //    "Transaction was successful. TransactionId is {0}",
                    //    res.Data.TransactionId));
                    return Request.CreateResponse(HttpStatusCode.OK, string.Format("Transaction was successful. TransactionId is {0}", res.Data.TransactionId));
                }
                else
                {
                    //Response.Write(string.Format(
                    //    "Transaction failed. Error code was {0}",
                    //    res.Data.ErrorCode));
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Format("Transaction failed. Error code was {0}", res.Data.ErrorCode));
                }
            }
            else
            {
                //Response.Write(string.Format(
                //    "Transaction failed. Error code was {0}",
                //    res.StatusCode));
                return Request.CreateResponse(HttpStatusCode.InternalServerError, string.Format("Transaction failed. Error code was {0}", res.StatusCode));
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
                var repository = new CrowdFundingTransactions();
                var transaction = await repository.ReadPayments(criteria);
                if (transaction.Result == TransResult.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.ReturnObject);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, transaction.Message);
            }
            catch (Exception e) { return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); }
        }
    }
}