using Com.OneSignal;
using Newtonsoft.Json;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.Views;
using NotiFit.Views.DashBoard;
using NotiFit.Views.Schedule;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NotiFit
{ 
    public partial class App : Application
    {
        #region Variable Declaration
        public static INavigation Navigation;
        public static Page DetailPage;
        public static NavigationPage NavigationPage { get;  set; }
        //public static Xamarin.Forms.Command Command { get; set; }
       // private string Url= "http://180.151.232.92:130/";
        #endregion
        public App ()
		{
			InitializeComponent();
            Settings.Url = Domain.Url;
            SetMainpage();
            OneSignal.Current.StartInit("0c5b7cb6-a33d-453e-ada0-2c54d17bd7ba")
                     .HandleNotificationOpened(HandleNotificationOpened)
                     .EndInit();
            
        }
        public void HandleNotificationOpened(Com.OneSignal.Abstractions.OSNotificationOpenedResult result)
        {
            App.NavigationPage.Navigation.PushAsync(new ScheduleListingPage());
        }
        
        private void SetMainpage()
        {
            try
            {

                //if (!string.IsNullOrEmpty(Settings.TokenCode))
                //{
                // NavigationPage = new NavigationPage(new AddNewSchedulePage());
                //    MainPage = NavigationPage;
                //}
                //else
                //{
                NavigationPage = new NavigationPage(new IntroductionPage())
                {
                    BarBackgroundColor = Color.FromHex("#1D8FD5"),
                    BarTextColor = Color.White
                    };
                    MainPage = NavigationPage;
                //}
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        //private void OnMessage(string serialiedCommand)
        //{
        //    MessagingCenter.Send<object, Xamarin.Forms.Command>(this, "Command", Command);
        //}

        //public static void DeviceToken(string token)
        //{
        ////    var main = Application.Current.MainPage as IntroductionPage;
        ////    main.SetToken(token);
        //}
        protected override void OnStart ()
		{
            //if(Command != null)
            //{
            //    App.NavigationPage.Navigation.PushAsync(new ScheduleListingPage());
            //}
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
