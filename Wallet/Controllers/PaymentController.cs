using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wallet.Models;
using Wallet.Repository;

namespace Wallet.Controllers
{
    public class PaymentController : ApiController
    {
        ProcessPayment process = new ProcessPayment();

        [HttpPost]
        public HttpResponseMessage Post([FromBody] PaymentModal payment)
        {
            process.SaveInfo(payment);
            return Request.CreateResponse(HttpStatusCode.OK, "Info Saved");
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var info = process.GetInfos();
            return Request.CreateResponse(HttpStatusCode.OK, info);
        }

    }
}
