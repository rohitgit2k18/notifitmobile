using AsNum.XFControls.Services;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.CustomeControls;
using NotiFit.Helpers;
using NotiFit.Views.DashBoard;
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
	public partial class LoginPage : ContentPage
	{
        #region Variable Declaration
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private bool IsValid { get; set; }
        private LoginRequest _objLoginRequest;
        private LoginResponse _objLoginResponse;
        private string _baseUrl;
        private RestApi _apiServices;
        #endregion
        public LoginPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            _objLoginResponse = new LoginResponse();
            _objLoginRequest = new LoginRequest();
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.LoginApiConstant;
            BindingContext = _objLoginRequest;
            if (Settings.RememberMe)
            {
                _objLoginRequest.Email = Settings.Email;
                _objLoginRequest.Password = Settings.Password;
            }
        }
        
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DependencyService.Get<IToast>().Show("No Network Connection!!");
                }
                else
                {
                    if (string.IsNullOrEmpty(_objLoginRequest.Email) || string.IsNullOrEmpty(_objLoginRequest.Password))
                    {
                        DependencyService.Get<IToast>().Show("Email and password both are Required!!");
                    }
                    else
                    {
                        if (!IsValid)
                        {
                            DependencyService.Get<IToast>().Show("Email is not in a correct format!");
                        }
                        else
                        {
                            await Navigation.PushPopupAsync(new LoadingPopPage());
                            _objLoginResponse = await _apiServices.LoginAsync(new Get_API_Url().CommonBaseApi(_baseUrl), false, new HeaderModel(), _objLoginRequest);
                            var Response = _objLoginResponse.Response;
                            if (Response.StatusCode == 200)
                            {
                                DependencyService.Get<IToast>().Show(Response.Message);
                                Settings.IsLoggedIn = true;
                                Settings.Email = _objLoginRequest.Email;
                                Settings.Password = _objLoginRequest.Password;
                                Settings.Name = Response.FirstName + Response.LastName;
                                Settings.TokenCode = Response.TokenCode;
                                Settings.UserID = Response.Id;
                                // await App.NavigationPage.Navigation.PushAsync(new HomePage());
                                var otherPage = new UserNavigationPage();
                                var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                                App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
                                await App.NavigationPage.PopToRootAsync(false);
                                await Navigation.PopAllPopupAsync();
                            }
                            else
                            {
                                await Navigation.PopAllPopupAsync();
                                await DisplayAlert("Message", "Incorrect Username or Password", "OK");
                                //DependencyService.Get<IToast>().Show(Response.Message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                await Navigation.PopAllPopupAsync();
                await DisplayAlert("Message", "Incorrect Username or Password", "OK");

            }
        }

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
           // EntryEmail.TextColor = IsValid ? Color.White : Color.Red;           
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            if (!Settings.RememberMe)
            {
                ChkbxRememberMe.Checked = true;
                Settings.RememberMe = true;
            }
            else
            {
                ChkbxRememberMe.Checked = false;
                Settings.RememberMe = false;
            }
        }

        private void BtnForgotPassword_Tapped(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new ForgotPasswordPage());
        }

        private void BtnRegisterUser_Clicked(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new UserRegistrationPage());
        }
    }
}