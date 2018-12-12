using AsNum.XFControls.Services;
using NotiFit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NotiFit.Services;
using NotiFit.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;

namespace NotiFit.Views.Notification
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyNotificationPage : ContentPage
	{
        #region Variavle Declaration
        public ObservableCollection<SetReminderList> setReminderLists { get; set; }
        public ObservableCollection<BillingReminderEntity> billingReminders { get; set; }
        public ObservableCollection<ListofFrequency> listofFrequencies { get; set; }
        private int SetReminderType;
        public string Via = null;
        public string SubsType = null;
        public string freq = null;
        GetPaymentPayPal getPaymentRequest;
        Sevices _services;
        ResponseGetPaymentPayPal getPaymentResponse;
        //private static string uriMyNotifit = "http://180.151.232.92:130/api/PayPal/GetPaymentPayPalURL_user";
        #endregion

        public MyNotificationPage()
        {
            InitializeComponent();
            try { 
            setReminderLists = new ObservableCollection<SetReminderList>();
            setReminderLists.Add(new SetReminderList { SetReminderId = 1, SetReminderName = "App" });
            setReminderLists.Add(new SetReminderList { SetReminderId = 2, SetReminderName = "SMS" });
            dropdownSetReminder.ItemsSource = setReminderLists;
                billingReminders = new ObservableCollection<BillingReminderEntity>();
                billingReminders.Add(new BillingReminderEntity { bilingRemTypeId = 1, bilingRemType = "Recurring" });
                billingReminders.Add(new BillingReminderEntity { bilingRemTypeId = 2, bilingRemType = "One off" });
                dropdownBillingReminder.ItemsSource = billingReminders;
                listofFrequencies = new ObservableCollection<ListofFrequency>();
                listofFrequencies.Add(new ListofFrequency { Id = 1, Frequency = "1 month" });                
                listofFrequencies.Add(new ListofFrequency { Id = 3, Frequency = "3 months" });               
                listofFrequencies.Add(new ListofFrequency { Id = 6, Frequency = "6 months" });               
                listofFrequencies.Add(new ListofFrequency { Id = 12, Frequency = "12 months" });
                dropdownFrequency.ItemsSource = listofFrequencies;

            }
            catch(Exception ex)
            {

            }
        }

        private void  dropdownbillingReminder_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as BillingReminderEntity;
                SetReminderType = WorkoutTypedata.bilingRemTypeId;
                Settings.BillingReminderName = WorkoutTypedata.bilingRemType;
                Settings.BillingReminderId = SetReminderType;
                SubsType = WorkoutTypedata.bilingRemType;
            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }

        private void dropdownSetReminder_SelectedIndexChanged(object sender, EventArgs e)
        {           

            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as SetReminderList;
                SetReminderType = WorkoutTypedata.SetReminderId;
                Settings.SetReminderName = WorkoutTypedata.SetReminderName;
                Settings.SetReminderId = SetReminderType;
                Via = WorkoutTypedata.SetReminderName;
            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }

        private async void dropdownFequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as ListofFrequency;
                SetReminderType = WorkoutTypedata.Id;
                Settings.FrequencyName = WorkoutTypedata.Frequency;
                Settings.FrequencyId = SetReminderType;
                freq = WorkoutTypedata.Frequency;
                await Navigation.PushPopupAsync(new LoadingPopPage());
                if ((Settings.SetReminderName != "null") && (Settings.BillingReminderName != "null") && (Settings.FrequencyName != "null"))
                {
                    if (Settings.BillingReminderName != "null")
                    {
                        if (Settings.FrequencyName != "null")
                        {
                            try
                            {
                            priceListReqModel _request = new priceListReqModel();
                            PriceModelResponse _response;
                            _request.SendVia = Settings.SetReminderName;
                            _request.Billing = Settings.BillingReminderName;
                            _request.Duration = Settings.FrequencyName;
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
                                    await Navigation.PopAllPopupAsync();
                                    Cost.Text = "Total New Subscriptions: US $" + res;
                                    //return _response;
                                }
                                else
                                {
                                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                                    _response = JsonConvert.DeserializeObject<PriceModelResponse>(ErrorResponse);
                                    DependencyService.Get<IToast>().Show("Error Occured");
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
                            await DisplayAlert("Alert", "Please select frequency", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Please select subscription type", "Ok");
                    }
                    //priceListReqModel _request = new priceListReqModel();
                    //PriceModelResponse _response;
                    //_request.SendVia = Settings.SetReminderName;
                    //_request.Billing = Settings.BillingReminderName;
                    //_request.Duration = Settings.FrequencyName;
                    //string s = JsonConvert.SerializeObject(_request);
                    //HttpResponseMessage response = null;
                    //string uri = "http://180.151.232.92:130/api/Price/GetPrice";
                    //using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
                    //{
                    //    HttpClient client = new HttpClient();
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                    //    response = await client.PostAsync(uri, stringContent);

                    //    if (response.IsSuccessStatusCode)
                    //    {
                    //        var SucessResponse = await response.Content.ReadAsStringAsync();
                    //        _response = JsonConvert.DeserializeObject<PriceModelResponse>(SucessResponse);
                    //        //Settings.PriceName = _response.Response.price;
                    //        var res = _response.Price;
                    //        Settings.PriceName = res;
                    //        await Navigation.PopAllPopupAsync();
                    //        Cost.Text = "Total New Subscriptions: US $" + res;
                    //        //return _response;
                    //    }
                    //    else
                    //    {
                    //        var ErrorResponse = await response.Content.ReadAsStringAsync();
                    //        _response = JsonConvert.DeserializeObject<PriceModelResponse>(ErrorResponse);
                    //        DependencyService.Get<IToast>().Show("Error Occured");
                    //        //return _response;
                    //        //Settings.PriceName = _response.Response.price;
                    //    }
                    //}
                }
                else
                {
                    await DisplayAlert("Alert", "Please select send via", "Ok");
                }
            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }

        //public void ClickTap1(object sender, EventArgs e)
        //{
        //   Navigation.PushAsync(new AddNotificationPage());
        //}

        public static bool result = false;
        public static AddNotificationPage addNotificationPage;
        public void ShowPaymentStatus(bool result)
        {
            DisplayAlert("Payment Successfull", "Thank You! Your payment is successfully", "Ok");
        }
        public async void MyNotificationSave_Click(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
            try
            {
                FriendNotification friendNotification = new FriendNotification();
                GetPaymentPayPal _request = new GetPaymentPayPal();
                string _response;
                List<ProductList> _productList = new List<ProductList>();
                ProductList _product = new ProductList();

                Up uP = new Up();
                _product.ProductID = 1;
                _product.Name = Settings.Name;
                _product.Description = Settings.BillingReminderName + "," + Settings.FrequencyName + "," + Settings.SetReminderName;
                _product.UnitPrice = Settings.PriceName;
                _product.SKU = "1";
                _product.OrderQty = 1;
                _productList.Add(_product);

                _request.ProductList=_productList;
                uP.Cost = Settings.PriceName;
                uP.DeliveryMethodId = Settings.SetReminderId;
                uP.DurationId = Settings.FrequencyId;
                uP.SubscriptionTypeId = Settings.BillingReminderId;
                uP.UserId = Convert.ToInt32(Settings.UserID);
                _request.up = uP;
                _request.SiteURL = "null";
                _request.InvoiceNumber = Guid.NewGuid().ToString();
                _request.Currency = "USD";
                _request.OrderDescription = Settings.UserID.ToString() ;
                _request.ShippingFee = "0";
                _request.Tax = "0";
                _request.UserId = (Settings.UserID).ToString();

                string s = JsonConvert.SerializeObject(_request);
                HttpResponseMessage response = null;
                string uri = "http://noti.fit:130/api/PayPal/GetPaymentPayPalURL_user";
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

                        //Settings.PriceName = _response.Response.pricefriendNotification
                        //var res = _response.Price;
                        //Settings.PriceName = res;

                        //Cost.Text = "Total New Subscriptions: US $" + res;
                        //return _response;
                    }
                    else
                    {
                        var ErrorResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<string>(ErrorResponse);
                        DependencyService.Get<IToast>().Show("Error Occured");
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
        public void Cancel_btn(object sender,EventArgs e)
        {
            Cost.Text = "";
            App.NavigationPage.Navigation.PopAsync(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (result)
            {
                Navigation.PopAsync();
                result = false;
            }
        }

        //public async void getPrice(object sender,EventArgs e)
        //{
        //    await Navigation.PushPopupAsync(new LoadingPopPage());
        //    if ((Settings.SetReminderName != "null") && (Settings.BillingReminderName != "null") && (Settings.FrequencyName != "null"))
        //    {
        //        if(Settings.BillingReminderName != "null")
        //        {
        //            if(Settings.FrequencyName != "null")
        //            {
        //                priceListReqModel _request = new priceListReqModel();
        //                PriceModelResponse _response;
        //                _request.SendVia = Settings.SetReminderName;
        //                _request.Billing = Settings.BillingReminderName;
        //                _request.Duration = Settings.FrequencyName;
        //                string s = JsonConvert.SerializeObject(_request);
        //                HttpResponseMessage response = null;
        //                string uri = "http://180.151.232.92:130/api/Price/GetPrice";
        //                using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //                {
        //                    HttpClient client = new HttpClient();
        //                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

        //                    response = await client.PostAsync(uri, stringContent);

        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        var SucessResponse = await response.Content.ReadAsStringAsync();
        //                        _response = JsonConvert.DeserializeObject<PriceModelResponse>(SucessResponse);
        //                        //Settings.PriceName = _response.Response.price;
        //                        var res = _response.Price;
        //                        Settings.PriceName = res;
        //                        await Navigation.PopAllPopupAsync();
        //                        Cost.Text = "Total New Subscriptions: US $" + res;
        //                        //return _response;
        //                    }
        //                    else
        //                    {
        //                        var ErrorResponse = await response.Content.ReadAsStringAsync();
        //                        _response = JsonConvert.DeserializeObject<PriceModelResponse>(ErrorResponse);
        //                        DependencyService.Get<IToast>().Show("Error Occured");
        //                        //return _response;
        //                        //Settings.PriceName = _response.Response.price;
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                await DisplayAlert("Alert", "Please select frequency", "Ok");
        //            }
        //        }
        //        else
        //        {
        //            await DisplayAlert("Alert", "Please select subscription type", "Ok");
        //        }
        //        //priceListReqModel _request = new priceListReqModel();
        //        //PriceModelResponse _response;
        //        //_request.SendVia = Settings.SetReminderName;
        //        //_request.Billing = Settings.BillingReminderName;
        //        //_request.Duration = Settings.FrequencyName;
        //        //string s = JsonConvert.SerializeObject(_request);
        //        //HttpResponseMessage response = null;
        //        //string uri = "http://180.151.232.92:130/api/Price/GetPrice";
        //        //using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //        //{
        //        //    HttpClient client = new HttpClient();
        //        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);
                    
        //        //    response = await client.PostAsync(uri, stringContent);

        //        //    if (response.IsSuccessStatusCode)
        //        //    {
        //        //        var SucessResponse = await response.Content.ReadAsStringAsync();
        //        //        _response = JsonConvert.DeserializeObject<PriceModelResponse>(SucessResponse);
        //        //        //Settings.PriceName = _response.Response.price;
        //        //        var res = _response.Price;
        //        //        Settings.PriceName = res;
        //        //        await Navigation.PopAllPopupAsync();
        //        //        Cost.Text = "Total New Subscriptions: US $" + res;
        //        //        //return _response;
        //        //    }
        //        //    else
        //        //    {
        //        //        var ErrorResponse = await response.Content.ReadAsStringAsync();
        //        //        _response = JsonConvert.DeserializeObject<PriceModelResponse>(ErrorResponse);
        //        //        DependencyService.Get<IToast>().Show("Error Occured");
        //        //        //return _response;
        //        //        //Settings.PriceName = _response.Response.price;
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        await DisplayAlert("Alert", "Please select send via", "Ok");
        //    }
        //}
    }
}