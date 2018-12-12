using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.Models
{
    public class NotificationFriend
    {
        public NotificationFriend()
        {
            Response = new FriendResponse();
        }

        public FriendResponse Response { get; set; }
    }
    public class ListofFriend
    {
        public int FriendId { get; set; }
        public int UserId { get; set; }
        public int DeliveryTypeId { get; set; }
        public string DeliveryType { get; set; }
        public string FriendsName { get; set; }
        public string DurationId { get; set; }
        public int TotalMonths { get; set; }
        public string PurchaseDate { get; set; }
        public string ExpiryDate { get; set; }
        public int SubscriptionTypeId { get; set; }
        public string SubcriptionType { get; set; }
        public double Cost { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int? CountryId { get; set; }
        public int PaymentDone { get; set; }
        public string SKU { get; set; }
    }

    public class FriendResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ListofFriend> listofFriends { get; set; }
    }

}
