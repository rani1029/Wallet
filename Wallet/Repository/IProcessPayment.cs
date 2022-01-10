using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models;

namespace Wallet.Repository
{
    public interface IProcessPayment
    {
        void SaveInfo(PaymentModal payment);
        List<PaymentModal> GetInfos();
    }
}
