using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notifit.Services.Models.ResponseModels;
using Notifit.Services.ApiHandler;
using TargetTransport.Models;
using Notifit.Services.Models;
using AsNum.XFControls.Services;
using Plugin.Connectivity;
using NotiFit.Helpers;

namespace NotiFit
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhoneNoCountryCodePage : PopupPage
    {
        #region Variable Declaration           
        private CountryListResponse _objCountryListResponse;
        private string _baseUrl;
        private RestApi _apiServices;
        #endregion
        public PhoneNoCountryCodePage ()
		{
			InitializeComponent ();
            _objCountryListResponse = new CountryListResponse();
            _apiServices = new RestApi();
            _baseUrl = Domain.GetCountryApiConstant;
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCountryData();
        }
        private async void LoadCountryData()
        {
            try
            {
                // 
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DependencyService.Get<IToast>().Show("No Network Connection!!");
                }
                else
                {
                   // await Navigation.PushPopupAsync(new LoadingPopPage());
                    _objCountryListResponse.Response = await _apiServices.GetAsyncData_GetCountryApiList(new Get_API_Url().GetCountryListApi(_baseUrl), false, new HeaderModel(), _objCountryListResponse.Response);

                    var result = _objCountryListResponse.Response;
                    if (result != null)
                    {
                        if (result.Count > 0)
                        {
                            listViewCountryList.ItemsSource = result;

                        }
                        else
                        {
                            DependencyService.Get<IToast>().Show("No Data!");
                            await Navigation.PopAllPopupAsync();
                        }

                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show("No Data!");
                        await Navigation.PopAllPopupAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }
        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
        
        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        private async void listViewCountryList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
              
            }
            else
            {
                var CountryData = e.SelectedItem as RootObject;
                Settings.CountryFlag = CountryData.flag;
                Settings.CountryCode = Convert.ToInt32(CountryData.callingCodes.FirstOrDefault());
                Settings.CountryName = CountryData.name;
                await Navigation.PopAllPopupAsync();
            }
            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }
    }
}