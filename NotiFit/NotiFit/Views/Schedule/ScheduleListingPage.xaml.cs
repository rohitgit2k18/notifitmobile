using AsNum.XFControls.Services;
using Newtonsoft.Json;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using NotiFit.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScheduleListingPage : ContentPage
	{
        #region Variable Seclaration
        private ScheduleListingRequest _objScheduleListingRequest;
        private ScheduleListingResponse _objScheduleListingResponse;
        private RestApi _apiService;
        private string _baseUrl;
        private HeaderModel _objHeaderModel;
        private ObservableCollection<FilterUser> filtersList { get; set; }
        List<string> objList = new List<string>();
        #endregion
      
        public ScheduleListingPage ()
		{
			InitializeComponent ();
            filtersList = new ObservableCollection<FilterUser>();
            filtersList.Add(new FilterUser { ListId = 1, ListName = "All items" });
            filtersList.Add(new FilterUser { ListId = 2, ListName = "Currently scheduled items" });
            filtersList.Add(new FilterUser { ListId = 3, ListName = "Ended schedules only" });
            filtersList.Add(new FilterUser { ListId = 4, ListName = "Future schedules only" });
            _objScheduleListingResponse = new ScheduleListingResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.GetScheduleListApiConstant;
            _objScheduleListingRequest = new ScheduleListingRequest();
            XFDDFilter.ItemsSource = filtersList;
            XFDDFilter.SelectedIndex = 0;
            //LoadSchdeuleListing();
            //for (int i = 0; i <= 10; i++)
            //{
            //    objList.Add("a");
            //}

            //WorkoutList.ItemsSource = objList;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadSchdeuleListing();
        }


        ScheduleListResponse result = new ScheduleListResponse();
        private async void LoadSchdeuleListing()
        {
            try
            {
                _objScheduleListingRequest = new ScheduleListingRequest
                {
                    UserId = Settings.UserID,
                    Search="All items"
                };
                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objScheduleListingResponse = await _apiService.GetScheduleListingAsync(new Get_API_Url().GetScheduleListApi(_baseUrl, _objScheduleListingRequest.Search), true, _objHeaderModel, _objScheduleListingRequest);
                result = _objScheduleListingResponse.Response;
                if (result.StatusCode == 200)
                {
                    if (result.scheduleLists.Count > 0)
                    {
                        WorkoutList.ItemsSource = result.scheduleLists;
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show("No Data!!");
                    }
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }

        } 

        private async void XFDDFilter_SelectedIndexChanged(object sender,EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var FilterTypedata = picker.SelectedItem as FilterUser;
                _objScheduleListingRequest.Search = FilterTypedata.ListName;
                _objScheduleListingRequest.UserId = Settings.UserID;
                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objScheduleListingResponse = await _apiService.GetScheduleListingAsync(new Get_API_Url().GetScheduleListApi(_baseUrl, _objScheduleListingRequest.Search), true, _objHeaderModel, _objScheduleListingRequest);
                result = _objScheduleListingResponse.Response;
                if (result.StatusCode == 200)
                {
                    if (result.scheduleLists.Count > 0)
                    {
                        WorkoutList.ItemsSource = result.scheduleLists;
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show("No Data!!");
                        await Navigation.PopAllPopupAsync();
                    }
                }
                else{
                    await Navigation.PopAllPopupAsync();
                    await DisplayAlert("Message", "No record found", "OK");
                }

            }
        }
        private void BtnNewscheduledWorkout_Clicked(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new AddNewSchedulePage());
        }

        private void WorkoutList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var obj = ((ItemTappedEventArgs)e).Item as ScheduleList;
            var WorkoutId = obj.ScheduleId;
            if (WorkoutId > 0)
            {
                App.NavigationPage.Navigation.PushAsync(new ScheduleDetailPage(WorkoutId));
            }
            else
            {
                DependencyService.Get<IToast>().Show("Error!");
            }
        }
        public async void Delete_Click(object sender, EventArgs e)
        {
            bool res = await DisplayAlert("Alert", "Are you sure you want to delete this Schedule?", "Yes","No");
            if(res == true)
            {
                await Navigation.PushPopupAsync(new LoadingPopPage());
                Button btndelete = ((Button)sender);
                Label lblDesc = ((Label)(((Grid)btndelete.Parent).Children.ToList().ElementAt(0)));

                string desc = lblDesc.Text;

                var item = result.scheduleLists.Where(x => x.Description.Equals(desc)).FirstOrDefault();
                int ScheduleId = item.ScheduleId;
                Common common = new Common();
                common.UserID = 1;
                SendUDIDEntityResponse _response = new SendUDIDEntityResponse();
                _response.Response = new Responses();

                string s = JsonConvert.SerializeObject(common);
                HttpResponseMessage response = null;
                string uri = "http://noti.fit:130/api/Schedule/DeleteSchedule/" + ScheduleId;
                using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
                {
                    string token = Settings.TokenCode;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    response = await client.PostAsync(uri, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var SucessResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<SendUDIDEntityResponse>(SucessResponse);
                        LoadSchdeuleListing();
                        await Navigation.PopAllPopupAsync();
                        DependencyService.Get<IToast>().Show("Successfully Removed");

                        //result.scheduleLists.Remove(item);


                        //WorkoutList.ItemsSource = result.scheduleLists;
                        //WorkoutList.IsRefreshing = true;
                        //WorkoutList.BeginRefresh();
                    }
                    else
                    {
                        var ErrorResponse = await response.Content.ReadAsStringAsync();
                        _response.Response.Message = JsonConvert.DeserializeObject<string>(ErrorResponse);
                        DependencyService.Get<IToast>().Show("Failed");
                    }
                }
            }
            else
            {

            }
            
        }
    }
}