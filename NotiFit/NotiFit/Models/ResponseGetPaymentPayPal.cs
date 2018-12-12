using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.Models
{
    public class ResponseGetPaymentPayPal
    {
        public ResponseGetPaymentPayPal()
        {
            Response = new NotificationDetail();
        }
        public string Url { get; set; }
        public string Message { get; set; }
        public NotificationDetail Response { get; set; }
        public int StatusCode { get; set; }       
    }
    public class NotificationDetail
    {
        public string DeliveryMethod { get; set; }
        public string DeliveryEmail { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SubscriptionType { get; set; }
        public string Cost { get; set; }
    }
}
