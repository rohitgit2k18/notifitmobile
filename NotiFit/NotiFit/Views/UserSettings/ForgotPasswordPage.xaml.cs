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
	public partial class ForgotPasswordPage : ContentPage
	{
        #region Variable Declaration
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private bool IsValid { get; set; }
        private ForgotPasswordRequest _objForgotPasswordRequest;
        private ForgotPasswordResponse _objForgotPasswordResponse;
        private string _baseUrl;
        private RestApi _apiServices;
        #endregion
        public ForgotPasswordPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            _objForgotPasswordRequest = new ForgotPasswordRequest();
            _objForgotPasswordResponse = new ForgotPasswordResponse();
            BindingContext = _objForgotPasswordRequest;
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.ForgotPasswordApiConstant;
        }

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }

        private async void BtnSendEmail_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DependencyService.Get<IToast>().Show("No Network Connection!!");
                }
                else
                {
                    if (string.IsNullOrEmpty(_objForgotPasswordRequest.Email))
                    {
                        DependencyService.Get<IToast>().Show("Email is Required!!");
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
                            _objForgotPasswordResponse = await _apiServices.ForgotPasswordAsync(new Get_API_Url().CommonBaseApi(_baseUrl), false, new HeaderModel(), _objForgotPasswordRequest);
                            var Response = _objForgotPasswordResponse;
                            if (Response.StatusCode == 200)
                            {
                                DependencyService.Get<IToast>().Show(Response.Message);
                                await Navigation.PopAllPopupAsync();
                                await App.NavigationPage.Navigation.PushAsync(new ConfirmOTPPage());
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
            catch(Exception ex)
            {
                var msg = ex.Message;
                await Navigation.PopAllPopupAsync();
            }
           
        }
    }
}