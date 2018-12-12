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
	public partial class PaymentGateway : ContentPage
	{
        MyNotificationPage notificationPage;
        FriendNotification friendNotification;

		public PaymentGateway(string responseGetPayment)
		{
			InitializeComponent ();

            //notificationPage = myNotificationPage;
            Navigation.PushPopupAsync(new LoadingPopPage());
            Webview.Source = responseGetPayment;
            Navigation.PopAllPopupAsync();

		}

        private void OnWebViewNavigated(object sender, WebNavigatedEventArgs e)
        {
            //var paymentId = e.Url;
            //if(paymentId.Contains(""))
        }

        private void OnWebViewNavigating(object sender, WebNavigatingEventArgs e)
        {
            var paymentId = e.Url;

            if (paymentId.Contains("103.14.127.78:4900"))
            {
                Navigation.PopAsync();

                if (paymentId.Contains("paymentId"))
                {
                    var pages = Navigation.NavigationStack.ToList();
                    Page parent = null;
                    if (pages != null && pages.Count > 0)
                        parent = pages.ElementAt(pages.Count - 2);

                    if (parent != null)
                        //{
                        //    if (parent.ToString().ToLower().Contains("mynotification"))
                        //    {
                        //        MyNotificationPage.result = true;
                        //        UserSettings.SettingsPage.settingsPage.ShowPaymentStatus(true);
                        //    }
                        //    else
                        //    {
                        //        AddNotificationPage.result = true;
                        //        DisplayAlert("Payment Successfull", "Thank You! Your payment is successfully", "Ok");
                        //    }
                        //}
                        if (parent.ToString().ToLower().Contains("mynotification"))
                        {
                            var queryValues = paymentId.Split('&').Select(q => q.Split('='))
                            .ToDictionary(k => k[0], v => v[1]);
                            var ID = queryValues.Values;
                            var pay = ID.FirstOrDefault();
                            //paymentmessageResponse = new PaymentmessageResponse();
                            //paymentmessageResponse.Message = Task.Run(async ()=> await restApi.GetAsyncData_GetApi("http://180.151.232.92:130/api/PayPal/GetPaymentDetails_User?paymentID="+pay, false, headerModel, paymentmessageResponse.Message)).Result ;

                            HttpResponseMessage response = null;
                            HttpClient client = new HttpClient();                            
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            response = client.GetAsync("http://noti.fit:130/api/PayPal/GetPaymentDetails_User?paymentID=" + pay).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var SucessResponse = response.Content.ReadAsStringAsync();
                                MyNotificationPage.result = true;
                                //UserSettings.SettingsPage.settingsPage.ShowPaymentStatus(true);
                                DisplayAlert("Payment Successfull", "Thank You! Your payment is successfully completed", "Ok");
                            }
                            else
                            {
                                var ErrorResponse = response.Content.ReadAsStringAsync();
                                DisplayAlert("Payment Failed", "Please try again", "Ok");
                            }

                        }
                        else
                        {
                            var queryValues = paymentId.Split('&').Select(q => q.Split('='))
                            .ToDictionary(k => k[0], v => v[1]);
                            var ID = queryValues.Values;
                            var pay = ID.FirstOrDefault();
                            HttpResponseMessage response = null;
                            HttpClient client = new HttpClient();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            response = client.GetAsync("http://noti.fit:130/api/PayPal/GetPaymentDetails?paymentID=" + pay).Result;
                            response = client.GetAsync("http://noti.fit:130/api/PayPal/GetPaymentDetails?paymentID=" + pay).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var SucessResponse = response.Content.ReadAsStringAsync();
                                AddNotificationPage.result = true;
                                DisplayAlert("Payment Successfull", "Thank You! Your payment is successfully completed", "Ok");
                            }
                            else
                            {
                                var ErrorResponse = response.Content.ReadAsStringAsync();
                                DisplayAlert("Payment Failed", "Please try again", "Ok");
                            }
                        }
                }

            }
                
            
        }
    }
}