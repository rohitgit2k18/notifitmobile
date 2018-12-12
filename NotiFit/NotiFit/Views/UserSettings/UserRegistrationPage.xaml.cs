using AsNum.XFControls.Services;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.UserSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserRegistrationPage : ContentPage
	{
        #region Variable Declaration    
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private bool IsValid { get; set; }
        private CountryListResponse _objCountryListResponse;
        private UserRegistrationRequest _objUserRegistrationRequest;
        private UserRegistrationResponse _objUserRegistrationResponse;
        private string _baseUrl;
        private string _baseUrlSubmitRecord;
        private RestApi _apiServices;
        #endregion

        public UserRegistrationPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
           // ImageCtryFlage.Source = "https://restcountries.eu/data/aut.svg";
            _objCountryListResponse = new CountryListResponse();
            _objUserRegistrationResponse = new UserRegistrationResponse();
            _objUserRegistrationRequest = new UserRegistrationRequest();
            BindingContext = _objUserRegistrationRequest;
            _apiServices = new RestApi();
            _baseUrl = Domain.GetCountryApiConstant;
            _baseUrlSubmitRecord = Settings.Url + Domain.UserSignUpApiConstant;

            LoadCountryData();

            Device.BeginInvokeOnMainThread(() => {
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (Settings.CountryCode > 0)
                    {
                        ImageCtryFlage.Source = Settings.CountryFlag;
                    }
                    return true;
                });
                //if (Settings.CountryCode > 0)
                //{
                //    ImageCtryFlage.Source = Settings.CountryFlag;
                //}
            });

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RootFrame.HeightRequest = -1;
            if (Settings.CountryCode>0)
            {
                ImageCtryFlage.Source = Settings.CountryFlag;
            }
        }
        private async void LoadCountryData()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DependencyService.Get<IToast>().Show("No Network Connection!!");
                }
                else
                {
                    await Navigation.PushPopupAsync(new LoadingPopPage());
                   // await Task.Delay(200);
                    _objCountryListResponse.Response = await _apiServices.GetAsyncData_GetCountryApiList(new Get_API_Url().GetCountryListApi(_baseUrl), false, new HeaderModel(), _objCountryListResponse.Response);
                    var result = _objCountryListResponse.Response;
                    if (result != null)
                    {
                        if (result.Count > 0)
                        {
                            // listViewCountryList.ItemsSource = result;
                            ImageCtryFlage.Source = result[13].flag;
                            Settings.CountryFlag = result[13].flag;
                            Settings.CountryCode = Convert.ToInt32(result[13].callingCodes.FirstOrDefault());
                        }
                        else
                        {
                            DependencyService.Get<IToast>().Show("No Data!");
                        }
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show("No Data!");
                        await Navigation.PopAllPopupAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }
        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {

        }

       

        private async void CountryListdd_Tapped(object sender, EventArgs e)
        {
          await  Navigation.PushPopupAsync(new PhoneNoCountryCodePage());
        }

       

        private async void BtnSignup_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DependencyService.Get<IToast>().Show("No Network Connection!!");
                }
                else
                {
                    _objUserRegistrationRequest.CountryId = Settings.CountryCode;
                    if (string.IsNullOrEmpty(_objUserRegistrationRequest.FirstName)||
                        string.IsNullOrEmpty(_objUserRegistrationRequest.LastName)||
                        string.IsNullOrEmpty(_objUserRegistrationRequest.Email)||
                        string.IsNullOrEmpty(_objUserRegistrationRequest.Password)||
                        string.IsNullOrEmpty(_objUserRegistrationRequest.MobileNumber)||
                        _objUserRegistrationRequest.CountryId < 0)
                    {
                        DependencyService.Get<IToast>().Show("All fields are Required!!");
                    }
                    else
                    {
                        if (!IsValid)
                        {
                            DependencyService.Get<IToast>().Show("Not a valid Email");
                        }
                        else
                        {
                            if(_objUserRegistrationRequest.Password.Length<6 || _objUserRegistrationRequest.Password.Length > 20)
                            {
                                DependencyService.Get<IToast>().Show("password Should be in between 6 to 20 character long!");
                            }
                            else
                            {
                                if (_objUserRegistrationRequest.MobileNumber.Length < 10 || _objUserRegistrationRequest.MobileNumber.Length> 11)
                                {
                                    DependencyService.Get<IToast>().Show("Mobile Number Should be 10 character long!");
                                }
                                else
                                {
                                    await Navigation.PushPopupAsync(new LoadingPopPage());
                                    _objUserRegistrationResponse = await _apiServices.UserRegistrationAsync(new Get_API_Url().CommonBaseApi(_baseUrlSubmitRecord), false, new HeaderModel(), _objUserRegistrationRequest);
                                    var Response = _objUserRegistrationResponse.Response;
                                    if (Response.StatusCode == 200)
                                    {
                                        DependencyService.Get<IToast>().Show(Response.Message);

                                        await App.NavigationPage.Navigation.PushAsync(new LoginPage());
                                        await Navigation.PopAllPopupAsync();
                                    }
                                    else
                                    {
                                        await Navigation.PopAllPopupAsync();
                                        DependencyService.Get<IToast>().Show(Response.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private void XFLblBacktoLogin_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void XFLblPrivecyPolicy_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrivacyPolicyPage());
        }
        private void XFLblTersAndCondition_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermsAndCondition());
        }
    }
}