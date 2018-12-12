using AsNum.XFControls.Services;
using Newtonsoft.Json;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using NotiFit.Models;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.UserSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfilePage : ContentPage
	{
        #region Variable Seclaration
        private MediaFile _mediaFile;
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private bool IsValid { get; set; }
        private EditProfileRequest _objEditProfileRequest;
        private EditProfileResponse  _objEditProfileResponse;
        private UpdateProfileResponse _objUpdateProfileResponse;
        private RestApi _apiService;
        private string _baseUrlEdit;
        private string _baseUrlUpdate;
        private string _baseUrl;
        private HeaderModel _objHeaderModel;
        private CountryListResponse _objCountryListResponse;
        private RestApi _apiServices;

        #endregion
        public UserProfilePage ()
		{
			InitializeComponent ();
            _objEditProfileResponse = new EditProfileResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _apiServices = new RestApi();
            _objCountryListResponse = new CountryListResponse();
            _baseUrlEdit = Settings.Url + Domain.EditUserApiConstant;
            _baseUrlUpdate = Settings.Url + Domain.UpdateUserApiConstant;
            _baseUrl = Domain.GetCountryApiConstant;
            LoadUserProfileData();
            LoadCountryData();
            _objUpdateProfileResponse = new UpdateProfileResponse();
            if (Settings.ProfilePicture == string.Empty)
            {
                imgProfile.Source = "profile.png";
            }
            else
            {
                imgProfile.Source = Settings.ProfilePicture;
            }
        }
        private async void LoadUserProfileData()
        {
            try
            {
                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objEditProfileRequest = new EditProfileRequest
                {
                    UserId = Convert.ToInt32(Settings.UserID)
                };
                //int UserId = Convert.ToInt32(Settings.UserID);

                EditProfileResponse objEditProfileResponse = new EditProfileResponse();
                _objEditProfileResponse = await _apiService.EditProfilePostAsync(new Get_API_Url().CommonBaseApi(_baseUrlEdit), true, _objHeaderModel, _objEditProfileRequest);
                //string s = JsonConvert.SerializeObject(_objEditProfileRequest);
                //HttpResponseMessage response = null;
                //string uri = "http://180.151.232.92:130/api/User/EditProfile";
                //using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
                //{
                    //HttpClient client = new HttpClient();
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);

                    //var geet = await client.PostAsync(uri, stringContent);

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var SucessResponse = await response.Content.ReadAsStringAsync();
                    //    objEditProfileResponse = JsonConvert.DeserializeObject<EditProfileResponse>(SucessResponse);
                    //    /return objEditProfileResponse;
                    //}
                    //else
                    //{
                    //    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    //    objEditProfileResponse = JsonConvert.DeserializeObject<EditProfileResponse>(ErrorResponse);
                    //    return objEditProfileResponse;
                    //}
                //}

                ProfileResponse result = _objEditProfileResponse.Response;
                if (result.StatusCode == 200)
                {
                    this.BindingContext = result.details;
                }
                else
                {
                    DependencyService.Get<IToast>().Show("No Data!!");
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
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
                            string countryId = (_objEditProfileResponse.Response.details.CountryId).ToString();
                            var Country = result.Where(f => f.callingCodes.FirstOrDefault() == countryId).Select(n => n.name);
                            CountryEntry.Text = Country.FirstOrDefault();
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

        private async void XFLblCountry_Tapped(object sender, EventArgs e)
        {
            CountryEntry.Text = Settings.CountryName;
            await Navigation.PushPopupAsync(new PhoneNoCountryCodePage());

        }


        private async void XFPickPhtoClickHere_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if(!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Alert", "No Photo Available", "OK");
                return;
            }
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
                return;
            imgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = _mediaFile.GetStream();
                Settings.ProfilePicture = _mediaFile.Path;
                var ImageName = _mediaFile.Path.Split('\\').LastOrDefault().Split('/').LastOrDefault();                
                return stream;
            });

            HttpResponseMessage response = null;
            HttpClient client = new HttpClient();
            UploadProfileResponse objUploadProfileResponse = new UploadProfileResponse();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(_mediaFile.GetStream()),
               "\"file\"",
               $"\"{_mediaFile.Path}\"");
            //  form.Add(new (content, 0, content.Count()), "profile_pic", Name);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.TokenCode);
            string uri = "http://noti.fit:130/api/Friend/UploadUserProfile";
            response = await client.PostAsync(uri, form);
            if (response.IsSuccessStatusCode)
            {
                var SucessResponse = await response.Content.ReadAsStringAsync();
                objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(SucessResponse);
                DependencyService.Get<IToast>().Show("Profile updated successfully !");
                //await DisplayAlert("Information", "Profile is updated successfully !", "Ok");
            }
            else
            {
                var ErrorResponse = await response.Content.ReadAsStringAsync();
                objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(ErrorResponse);
                DependencyService.Get<IToast>().Show("Failed !");
            }


        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    DependencyService.Get<IToast>().Show("No Network Connection!!");
                }
                else
                {
                    if (string.IsNullOrEmpty(_objEditProfileResponse.Response.details.Email) || string.IsNullOrEmpty(_objEditProfileResponse.Response.details.FirstName)
                        || string.IsNullOrEmpty(_objEditProfileResponse.Response.details.MobileNumber))
                    {
                        DependencyService.Get<IToast>().Show("Email FirstName and Phone Number is  Required!!");
                    }
                    else
                    {
                        if (!IsValid)
                        {
                            DependencyService.Get<IToast>().Show("Email is not in a correct format!");
                        }
                        else
                        {
                            _objEditProfileResponse.Response.details.CountryId = Settings.CountryCode;
                            _objHeaderModel.TokenCode = Settings.TokenCode;
                            await Navigation.PushPopupAsync(new LoadingPopPage());
                            _objUpdateProfileResponse = await _apiService.UpdateProfileAsync(new Get_API_Url().CommonBaseApi(_baseUrlUpdate), true, _objHeaderModel, _objEditProfileResponse);

                            if (_objUpdateProfileResponse.StatusCode == 200)
                            {
                                Settings.Name = _objEditProfileResponse.Response.details.FirstName;
                                await Navigation.PopAllPopupAsync();
                                DependencyService.Get<IToast>().Show(_objUpdateProfileResponse.Message);
                                
                                //var otherPage = new UserNavigationPage();
                                //var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                                //App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);

                            }
                            else
                            {
                                await Navigation.PopAllPopupAsync();
                                DependencyService.Get<IToast>().Show(_objUpdateProfileResponse.Message);
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
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PopAsync();
        }
        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));                
        }
       
    }
}