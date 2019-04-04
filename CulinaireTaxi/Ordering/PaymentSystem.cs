using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaireTaxi.Pages
{
    public static class PaymentSystem
    {

    }

    public class SkrillPaymentData
    {
        public string Currency;
        public string paymentInfo;

        public string paymentMethod;

        public string succesURL = "https://pay.skrill.com/payment_made.html";
        public string cancelURL = "https://pay.skrill.com/payment_cancelled.html";

        public string paytoEmail;
        public string payFromEmail;

        public decimal amount;

        public override string ToString()
        {
            return $"pay_to_email={paytoEmail}&return_url={succesURL}&cancel_url={cancelURL}&pay_from_email={payFromEmail}&amount={amount}&payment_methods={paymentMethod}";
        }
    }
}
