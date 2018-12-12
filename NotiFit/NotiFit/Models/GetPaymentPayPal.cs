using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.Models
{
    public class GetPaymentPayPal
    {
        public List<ProductList> ProductList { get; set; }
        public Up up { get; set; }
        public string SiteURL { get; set; }
        public string InvoiceNumber { get; set; }
        public string Currency { get; set; }
        public string Tax { get; set; }
        public string ShippingFee { get; set; }
        public string OrderDescription { get; set; }
        public string PayerID { get; set; }
        public string PaymentID { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
        public string StartID { get; set; }
        public int StartIndex { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartDate { get; set; }
        public string PayeeEmail { get; set; }
        public string PayeeID { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
    public class ProductList
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public string SKU { get; set; }
        public int OrderQty { get; set; }
    }

    public class Up
    {
        public int DeliveryMethodId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int DurationId { get; set; }
        public double Cost { get; set; }
        public int UserId { get; set; }
    }
    
}
