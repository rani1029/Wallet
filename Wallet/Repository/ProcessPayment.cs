using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Wallet.Models;

namespace Wallet.Repository
{
    public class ProcessPayment : IProcessPayment
    {
        public void SaveInfo(PaymentModal payment)
        {
            var filepath = @"C:\Users\admin\source\repos\Wallet\Info.json";
            string jsonstr = File.ReadAllText(filepath);
            List<PaymentModal> paymentList;
            // paymentList.Add(new PaymentModal { PhoneNumber = 00000, UpiId = "default" });
            if (jsonstr.Length != 0)
            {
                paymentList = (List<PaymentModal>)JsonConvert.DeserializeObject<List<PaymentModal>>(jsonstr);
            }
            else
            {
                paymentList = new List<PaymentModal>();
            }

            PaymentModal pay = new PaymentModal();
            pay.PhoneNumber = payment.PhoneNumber;
            pay.UpiId = payment.UpiId;
            //  List<PaymentModal> paymentList = new List<PaymentModal> { pay };
            if (paymentList == null)
            {
                paymentList = new List<PaymentModal>();
            }
            paymentList.Add(pay);
            string jsoncontact = JsonConvert.SerializeObject(paymentList, Formatting.Indented);
            File.WriteAllText(filepath, jsoncontact);
        }

        public List<PaymentModal> GetInfos()
        {
            var path = @"C:\Users\admin\source\repos\Wallet\Info.json";
            string Infojson = File.ReadAllText(path);
            List<PaymentModal> infoList = (List<PaymentModal>)JsonConvert.DeserializeObject<List<PaymentModal>>(Infojson);
            return infoList;
        }
    }
}