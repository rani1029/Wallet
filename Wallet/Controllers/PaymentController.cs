using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Wallet.Models;
using Wallet.Repository;

namespace Wallet.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class PaymentController : ApiController
    {
        ProcessPayment process = new ProcessPayment();

        [HttpPost]
        public IHttpActionResult Post([FromBody] PaymentModal payment)
        {
            process.SaveInfo(payment);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var info = process.GetInfos();
            return Ok(info);
        }

        [HttpPost]
        public IHttpActionResult PaytmTransfer([FromBody] TransferMoney transfer)
        {
            process.TransactionMoney(transfer);
            return Ok();
        }

    }
}
