using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wallet.Models
{
    public class TransferMoney
    {
        public string BeneficieryMobile { get; set; }
        public string UpiId { get; set; }
        public string Amount { get; set; }
        public string Solution { get; set; }
    }
}