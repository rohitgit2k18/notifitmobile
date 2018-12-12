using Newtonsoft.Json;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.ViewModels;
using NotiFit.Views.UserSettings;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IntroductionPage : ContentPage
	{
        public string deviceToken;
        #region Variable Declaration
        private IntroductionVIewModel _objIntroductionVIewModel;
        #endregion
        public IntroductionPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            _objIntroductionVIewModel = new IntroductionVIewModel();
            BindingContext = _objIntroductionVIewModel;
            DeviceNotification();

        }
        public void SetToken(string token)
        {
            deviceToken = token;
        }
        public async void DeviceNotification()
        {
            string title = "PUSH_NOTIFICATION";
            string messageSend = "TESTING";
            Common common = new Common();
            common.UserID = 1;
            string s = JsonConvert.SerializeObject(common);
            HttpResponseMessage response = null;
            string uri = "http://noti.fit:130/api/PushNotification/MsgNotification?deviceId=" + deviceToken + "&NotificationTitle=" + title + "&MessageSend=" + messageSend;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                response = await client.PostAsync(uri, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    
                }
            }
        }


        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new LoginPage());
        }

        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
           App.NavigationPage.Navigation.PushAsync(new UserRegistrationPage());
        }
    }
}