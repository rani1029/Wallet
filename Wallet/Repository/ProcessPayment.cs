using Newtonsoft.Json;
using Paytm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public string TransactionMoney(TransferMoney payment)
        {
            string OrderId = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();

            requestBody.Add("subwalletGuid", "28054249-XXXX-XXXX-af8f-fa163e429e83");
            requestBody.Add("orderId", OrderId);
            requestBody.Add("beneficiaryPhoneNo", payment.BeneficieryMobile);
            requestBody.Add("amount", payment.Amount);

            string post_data = JsonConvert.SerializeObject(requestBody);

            /*
            * Generate checksum by parameters we have in body
            * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
            */
            string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(requestBody), "YOUR_KEY_HERE");

            string x_mid = "YOUR_MID_HERE";
            string x_checksum = paytmChecksum;

            //For  Staging
            // string url = "https://staging-dashboard.paytm.com/bpay/api/v1/disburse/order/wallet/{solution}";

            //For  Production 
            string url = "https://dashboard.paytm.com/bpay/api/v1/disburse/order/wallet/" + payment.Solution;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = post_data.Length;
            webRequest.Headers.Add("x-mid", x_mid);
            webRequest.Headers.Add("x-checksum", x_checksum);
            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(post_data);
            }

            string responseData = string.Empty;

            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
            }
            return responseData;
        }
    }
}