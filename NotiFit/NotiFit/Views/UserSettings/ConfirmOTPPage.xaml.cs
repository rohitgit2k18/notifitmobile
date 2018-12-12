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
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.UserSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmOTPPage : ContentPage
	{
        #region Variable Declaration                
        private ResetPasswordRequest _objResetPasswordRequest;
        private ResetPasswordResponse _objResetPasswordResponse;
        private string _baseUrl;
        private RestApi _apiServices;
        #endregion
        public ConfirmOTPPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            _objResetPasswordRequest = new ResetPasswordRequest();
            _objResetPasswordResponse = new ResetPasswordResponse();
            BindingContext = _objResetPasswordRequest;
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.ResetPasswordApiConstant;
        }

        private async void BtnSendpassword_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DependencyService.Get<IToast>().Show("No Network Connection!!");
                }
                else
                {
                    if (string.IsNullOrEmpty(_objResetPasswordRequest.Password)||string.IsNullOrEmpty(_objResetPasswordRequest.ConfirmPassword)|| _objResetPasswordRequest.OTP<=0)
                    {
                        DependencyService.Get<IToast>().Show("Email is Required!!");
                    }
                    else
                    {
                        if (_objResetPasswordRequest.Password == _objResetPasswordRequest.ConfirmPassword)
                        {
                            await Navigation.PushPopupAsync(new LoadingPopPage());
                            _objResetPasswordResponse = await _apiServices.ResetPasswordAsync(new Get_API_Url().CommonBaseApi(_baseUrl), false, new HeaderModel(), _objResetPasswordRequest);
                            var Response = _objResetPasswordResponse;
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
                        else
                        {
                            DependencyService.Get<IToast>().Show("New Password & Confirm Password didn't Match!! ");
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
    }
}