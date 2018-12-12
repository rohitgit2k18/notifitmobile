using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.Models
{
    public class NotificationFriendSave
    {
        public int UserId { get; set; }
        public int DeliveryTypeId { get; set; }
        public string FriendsName { get; set; }
        public int DurationId { get; set; }
        public int CountryId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SubscriptionTypeId { get; set; }
        public double Cost { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
}
