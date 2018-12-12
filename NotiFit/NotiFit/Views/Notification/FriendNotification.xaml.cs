using AsNum.XFControls.Services;
using Newtonsoft.Json;
using NotiFit.Helpers;
using NotiFit.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
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
    public partial class FriendNotification : ContentPage
    {
        private NotificationFriend _response;
        public double TotalCost = 0;
        public FriendNotification()
        {
            InitializeComponent();
            getListofInvitedFrnd();
            //BindListView(NotificationFriend list);
            // lstView.ItemsSource = _response.Response.listofFriends;
            //foreach(var item in response)            
        }
        public async Task<NotificationFriend>  getListofInvitedFrnd()
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
            Common common = new Common();
            common.UserID = 1;
            NotificationFriend _response ;
            string s = JsonConvert.SerializeObject(common);
            HttpResponseMessage response = null;
            string uri = "http://noti.fit:130/api/Friend/ListOfInvitedFriend";
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                response = await client.PostAsync(uri, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    _response = JsonConvert.DeserializeObject<NotificationFriend>(SucessResponse);
                    List<ListofFriend> unPaidList = new List<ListofFriend>();                    
                    foreach(var i in _response.Response.listofFriends)
                    {
                        if(i.PaymentDone > 0)
                        {
                            
                        }
                        else
                        {
                            var cost = i.Cost;
                            TotalCost = TotalCost + cost;
                            unPaidList.Add(i);
                        }
                    }
                    await Navigation.PopAllPopupAsync();
                    lstView.ItemsSource = unPaidList;

                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    _response = JsonConvert.DeserializeObject<NotificationFriend>(ErrorResponse);
                }
                

                return _response;

            }
        }

       

        public async void getTotalPrice(object sender, EventArgs e) 
        {
            Cost.Text = "Total New Subscription: US $"+TotalCost;
        }
       
        public async void getPaymentGateway_Click(object sender, EventArgs e)
        {
           
            try
            {
                await Navigation.PushPopupAsync(new LoadingPopPage());
                MyNotificationPage myNotification = new MyNotificationPage();
                GetPaymentPayPal _request = new GetPaymentPayPal();
                string _response;
                List<ProductList> _productList = new List<ProductList>();

                NotificationFriend notificationFriendList = new NotificationFriend();
                notificationFriendList = await getListofInvitedFrnd();
                Up uP = new Up();
                foreach(var item in notificationFriendList.Response.listofFriends)
                {
                    if(item.PaymentDone == 0)
                    { 
                        ProductList _product = new ProductList();
                        _product.ProductID = 1;
                        _product.Name = item.FriendsName;
                        _product.Description = item.DeliveryType + "," + item.TotalMonths + "," + item.SubcriptionType;
                        _product.UnitPrice = item.Cost;
                        _product.SKU = item.SKU;
                        _product.OrderQty = 1;
                        _productList.Add(_product);
                    }

                }

                //_product.ProductID = 1;
                //_product.Name = Settings.FriendName;
                //_product.Description = Settings.DeliveryMethodName + "," + Settings.DurationName + "," + Settings.SubscriptionTypeName;
                //_product.UnitPrice = Settings.PriceName;
                //_product.SKU = "1";
                //_product.OrderQty = 1;
                //_productList.Add(_product);

                _request.ProductList = _productList;
                uP.Cost = TotalCost;
                uP.DeliveryMethodId = Settings.DeliveryMethodId;
                uP.DurationId = Settings.DurationId;
                uP.SubscriptionTypeId = Settings.SubscriptionTypeId;
                uP.UserId = Convert.ToInt32(Settings.UserID);
                _request.up = uP;
                _request.SiteURL = "null";
                _request.InvoiceNumber = Guid.NewGuid().ToString();
                _request.Currency = "USD";
                _request.OrderDescription = Settings.UserID.ToString();
                _request.ShippingFee = "0";
                _request.Tax = "0";
                _request.PayerID = (Settings.UserID).ToString();
                _request.PaymentID = "null";
                _request.Count = 1;
                _request.UserId = (Settings.UserID).ToString();
                _request.StartID = "null";
                _request.StartIndex = 1;
                _request.StartTime = "null";
                _request.EndTime = "null";
                _request.StartDate = "null";
                _request.PayeeEmail = Settings.Email;
                _request.PayeeID = (Settings.UserID).ToString();
                _request.SortBy = "Ascending";
                _request.SortOrder = "Albhabetical";

                string s = JsonConvert.SerializeObject(_request);
                HttpResponseMessage response = null;
                string uri = "http://noti.fit:130/api/PayPal/GetPaymentPayPalURL";
                using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                    response = await client.PostAsync(uri, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var SucessResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<string>(SucessResponse);
                        await Navigation.PopAllPopupAsync();
                        await Navigation.PushAsync(new PaymentGateway(_response));

                        //Settings.PriceName = _response.Response.price;
                        //var res = _response.Price;
                        //Settings.PriceName = res;

                        //Cost.Text = "Total New Subscriptions: US $" + res;
                        //return _response;
                    }
                    else
                    {
                        var ErrorResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<string>(ErrorResponse);
                        await Navigation.PopAllPopupAsync();
                        DependencyService.Get<IToast>().Show("Error");
                        //return _response;
                        //Settings.PriceName = _response.Response.price;
                    }

                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        } 
        public void cancel_Friend(object sender,EventArgs e)
        {
            
        }

    }
}