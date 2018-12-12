using AsNum.XFControls.Services;
using Newtonsoft.Json;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.Views.UserSettings;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Notification
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNotificationPage : ContentPage
	{
        public DateTime expirydate;
        public ObservableCollection<SetReminderList> deliveryMethodList { get; set; }
        public ObservableCollection<BillingReminderEntity> supscriptionTypeList { get; set; }
        public ObservableCollection<ListofFrequency> durationList { get; set; }
        private int SetReminderType;
        public AddNotificationPage ()
		{
			InitializeComponent ();
            try
            {
                deliveryMethodList = new ObservableCollection<SetReminderList>();
                deliveryMethodList.Add(new SetReminderList { SetReminderId = 1, SetReminderName = "App" });
                deliveryMethodList.Add(new SetReminderList { SetReminderId = 2, SetReminderName = "SMS" });
                dropdownDeliveryMethod.ItemsSource = deliveryMethodList;
                supscriptionTypeList = new ObservableCollection<BillingReminderEntity>();
                supscriptionTypeList.Add(new BillingReminderEntity { bilingRemTypeId = 1, bilingRemType = "Recurring" });
                supscriptionTypeList.Add(new BillingReminderEntity { bilingRemTypeId = 2, bilingRemType = "One off" });
                dropdownSubscriptionType.ItemsSource = supscriptionTypeList;
                durationList = new ObservableCollection<ListofFrequency>();
                durationList.Add(new ListofFrequency { Id = 1, Frequency = "1 month" });                
                durationList.Add(new ListofFrequency { Id = 3, Frequency = "3 months" });                
                durationList.Add(new ListofFrequency { Id = 6, Frequency = "6 months" });                            
                durationList.Add(new ListofFrequency { Id = 12, Frequency = "12 months" });
                dropdownDuration.ItemsSource = durationList;
                EnterEmailPhone.Text = "Enter Email*";

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
		}

        private void dropdownDeliveryMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as SetReminderList;
                SetReminderType = WorkoutTypedata.SetReminderId;
                Settings.DeliveryMethodName = WorkoutTypedata.SetReminderName;
                Settings.DeliveryMethodId = SetReminderType;
                if(Settings.DeliveryMethodName == "SMS")
                {
                    EnterEmailPhone.Text = "Enter Phone*";
                }
                if (Settings.DeliveryMethodName == "App")
                {
                    EnterEmailPhone.Text = "Enter Email*";
                }
            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }
        private async void dropdownDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                try{
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as ListofFrequency;
                SetReminderType = WorkoutTypedata.Id;
                Settings.DurationName = WorkoutTypedata.Frequency;
                Settings.DurationId = SetReminderType;
                expirydate = XFDPstartDate.Date.AddMonths(Settings.DurationId);
                XFDPEndDate.Date = expirydate;
                priceListReqModel _request = new priceListReqModel();
                PriceModelResponse _response;
                _request.SendVia = Settings.DeliveryMethodName;
                _request.Billing = Settings.SubscriptionTypeName;
                _request.Duration = Settings.DurationName;
                string s = JsonConvert.SerializeObject(_request);
                HttpResponseMessage response = null;
                string uri = "http://noti.fit:130/api/Price/GetPrice";
                using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                    response = await client.PostAsync(uri, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var SucessResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<PriceModelResponse>(SucessResponse);
                        //Settings.PriceName = _response.Response.price;
                        var res = _response.Price;
                        Settings.PriceName = res;

                        CostofPlan.Text = "Total New Subscriptions: US $" + res;
                        await Navigation.PopAllPopupAsync();
                        //return _response;
                    }
                    else
                    {
                        var ErrorResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<PriceModelResponse>(ErrorResponse);
                        await DisplayAlert("Alert", "Please select the all dropdown", "Ok");
                        //return _response;
                        //Settings.PriceName = _response.Response.price;
                    }
                }
                }
                catch(Exception ex)
                {
                    //await Navigation.PopAsync(true);
                    await Navigation.PopAllPopupAsync();
                }
            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }
        private void dropdownSubscriptionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as BillingReminderEntity;
                SetReminderType = WorkoutTypedata.bilingRemTypeId;
                Settings.SubscriptionTypeName = WorkoutTypedata.bilingRemType;
                Settings.SubscriptionTypeId = SetReminderType;
            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }

        //public async void getPrices(object sender, EventArgs e)
        //{
        //    //if ((Settings.BillingReminderName != null) && (Settings.FrequencyName != null) && (Settings.SetReminderName != null))
        //    //{
            
        //    //}
        //}
        public async void FrndNotifitSave(object sender, EventArgs e)
        {
            //List<NotificationFriend> listnotifit = new List<NotificationFriend>();
            try
            {
                NotificationFriendSave notificationFriendSave = new NotificationFriendSave();
                notificationFriendSave.DeliveryTypeId = Settings.DeliveryMethodId;
                if(Settings.DeliveryMethodName == "SMS")
                {
                    notificationFriendSave.MobileNumber = Email.Text;
                }
                else{
                    notificationFriendSave.Email = Email.Text;
                }
                notificationFriendSave.FriendsName = FriendName.Text;
                notificationFriendSave.DurationId = Settings.DurationId;
                notificationFriendSave.Cost = Settings.PriceName;
                notificationFriendSave.ExpiryDate = XFDPEndDate.Date;
                //notificationFriendSave.ExpiryDate = DateTime.ParseExact(expiry_date, "ddmmyyyy", CultureInfo.InvariantCulture);
                //var purchase_date = 
                notificationFriendSave.PurchaseDate = XFDPstartDate.Date;
                notificationFriendSave.SubscriptionTypeId = Settings.SubscriptionTypeId;
                notificationFriendSave.UserId = Convert.ToInt16(Settings.UserID);
                //notificationFriendSave.MobileNumber = Settings.PhoneNo;
                var _response = "null";
                string s = JsonConvert.SerializeObject(notificationFriendSave);
                HttpResponseMessage response = null;
                string uri = "http://noti.fit:130/api/Friend/AddFriendInvitation";
                using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                    response = await client.PostAsync(uri, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var SucessResponse = await response.Content.ReadAsStringAsync();
                        DependencyService.Get<IToast>().Show("Successfully Save!");
                        //await Navigation.PushAsync(new FriendNotification());
                    }
                    else
                    {
                        var ErrorResponse = await response.Content.ReadAsStringAsync();
                        DependencyService.Get<IToast>().Show("Something Wrong!");
                        // _response = JsonConvert.DeserializeObject<string>(ErrorResponse);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            //NotificationFriend notificationFriend = new NotificationFriend();
            



            //notificationFriend.deliveryEmail = Email.Text;
            //notificationFriend.friendName = FriendName.Text;
            //Settings.FriendName = FriendName.Text;
            //notificationFriend.duration = Settings.DurationName;
            //notificationFriend.subscriptionType = Settings.SubscriptionTypeName;
            //notificationFriend.purchaseDate = XFDPstartDate.Date.Day + "/" + XFDPstartDate.Date.Month +"/"+ XFDPstartDate.Date.Year;
            //notificationFriend.expiryDate = XFDPEndDate.Date.Day+"/"+ XFDPEndDate.Date.Month+"/"+ XFDPEndDate.Date.Year;
            //notificationFriend.cost = Settings.PriceName;
            //listnotifit.Add(notificationFriend);
            //await Navigation.PushAsync(new FriendNotification());
            //try
            //{
            //    GetPaymentPayPal _request = new GetPaymentPayPal();
            //    string _response;
            //    List<ProductList> _productList = new List<ProductList>();
            //    ProductList _product = new ProductList();

            //    Up uP = new Up();
            //    _product.ProductID = 1;
            //    _product.Name = FriendName.Text;
            //    _product.Description = Settings.SubscriptionTypeName + "," + Settings.DurationName + "," + Settings.DeliveryMethodName;
            //    _product.UnitPrice = Settings.PriceName;
            //    _product.SKU = "1";
            //    _product.OrderQty = 1;
            //    _productList.Add(_product);

            //    _request.ProductList = _productList;
            //    uP.Cost = Settings.PriceName;
            //    uP.DeliveryMethodId = Settings.DeliveryMethodId;
            //    uP.DurationId = Settings.DurationId;
            //    uP.SubscriptionTypeId = Settings.SubscriptionTypeId;
            //    uP.UserId = Convert.ToInt32(Settings.UserID);
            //    _request.up = uP;
            //    _request.SiteURL = "null";
            //    _request.InvoiceNumber = Guid.NewGuid().ToString();
            //    _request.Currency = "USD";
            //    _request.OrderDescription = Settings.UserID.ToString();
            //    _request.ShippingFee = "0";
            //    _request.Tax = "0";

            //    string s = JsonConvert.SerializeObject(_request);
            //    HttpResponseMessage response = null;
            //    string uri = "http://180.151.232.92:130/api/PayPal/GetPaymentPayPalURL_user";
            //    using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            //    {
            //        HttpClient client = new HttpClient();
            //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

            //        response = await client.PostAsync(uri, stringContent);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            var SucessResponse = await response.Content.ReadAsStringAsync();
            //            _response = JsonConvert.DeserializeObject<string>(SucessResponse);
            //            await Navigation.PushAsync(new PaymentGateway(_response));

            //            //Settings.PriceName = _response.Response.price;
            //            //var res = _response.Price;
            //            //Settings.PriceName = res;

            //            //Cost.Text = "Total New Subscriptions: US $" + res;
            //            //return _response;
            //        }
            //        else
            //        {
            //            var ErrorResponse = await response.Content.ReadAsStringAsync();
            //            _response = JsonConvert.DeserializeObject<string>(ErrorResponse);
            //            //return _response;
            //            //Settings.PriceName = _response.Response.price;
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    var msg = ex.Message;
            //}
        }
        public void ClickTap1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FriendNotification());
        }
        public static bool result = false;
        public static AddNotificationPage addNotificationPage;
        public void ShowPaymentStatus(bool result)
        {
            DisplayAlert("Payment Successfull", "Thank You! Your payment is successfully", "Ok");
        }

        public void cancel_Click(object sender,EventArgs e)
        {
            //deliveryMethodList = new ObservableCollection<SetReminderList>();
            //deliveryMethodList.Add(new SetReminderList { SetReminderId = 1, SetReminderName = "App" });
            //deliveryMethodList.Add(new SetReminderList { SetReminderId = 2, SetReminderName = "SMS" });
            //dropdownDeliveryMethod.ItemsSource = deliveryMethodList;
            //supscriptionTypeList = new ObservableCollection<BillingReminderEntity>();
            //supscriptionTypeList.Add(new BillingReminderEntity { bilingRemTypeId = 1, bilingRemType = "Recurring" });
            //supscriptionTypeList.Add(new BillingReminderEntity { bilingRemTypeId = 2, bilingRemType = "One off" });
            //dropdownSubscriptionType.ItemsSource = supscriptionTypeList;
            //durationList = new ObservableCollection<ListofFrequency>();
            //durationList.Add(new ListofFrequency { Id = 1, Frequency = "1 month" });
            //durationList.Add(new ListofFrequency { Id = 3, Frequency = "3 months" });
            //durationList.Add(new ListofFrequency { Id = 6, Frequency = "6 months" });
            //durationList.Add(new ListofFrequency { Id = 12, Frequency = "12 months" });
            //dropdownDuration.ItemsSource = durationList;
            Email.Text = "";
            FriendName.Text = "";
            CostofPlan.Text = "";
            App.NavigationPage.Navigation.PopAsync(true);
        }
    }
}