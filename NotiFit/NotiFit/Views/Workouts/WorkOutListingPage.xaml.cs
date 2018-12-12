using AsNum.XFControls.Services;
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
using System.Text;
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Workouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOutListingPage : ContentPage
    {
        #region Variable Seclaration
        private ExerciseListingRequest _objExerciseListingRequest;
        private ExerciseListingResponse _objExerciseListingResponse;
        private RestApi _apiService;
        private string _baseUrl;
        private HeaderModel _objHeaderModel;
        private ObservableCollection<FilterUser> filtersList { get; set; }
        private string Search { get; set; }
        List<string> objList = new List<string>();
        #endregion

        public WorkOutListingPage()
        {
            InitializeComponent();
            filtersList = new ObservableCollection<FilterUser>();
            filtersList.Add(new FilterUser { ListId = 1, ListName = "Current week (Monday to Sunday)" });
            filtersList.Add(new FilterUser { ListId = 2, ListName = "Current week(Sunday to Saturday)" });
            filtersList.Add(new FilterUser { ListId = 3, ListName = "Next seven days" });
            filtersList.Add(new FilterUser { ListId = 4, ListName = "Previous seven days" });
            _objExerciseListingResponse = new ExerciseListingResponse();
            _apiService = new RestApi();
            _baseUrl = Settings.Url + Domain.ListofWorkoutApiConstant;
            _objHeaderModel = new HeaderModel();
            XFDDWorkoutFilter.ItemsSource = filtersList;
            XFDDWorkoutFilter.SelectedIndex = 0;
            Search = "Current week (Monday to Sunday)";
            //LoadWorkoutListData();
            //for(int i = 0; i <= 10; i++)
            //{
            //    objList.Add("a");
            //}

            //WorkoutList.ItemsSource = objList;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadWorkoutListData();
        }
        private async void LoadWorkoutListData()
        {
            try
            {
                _objExerciseListingRequest = new ExerciseListingRequest
                {
                    UserId = Settings.UserID
                };

                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objExerciseListingResponse = await _apiService.ListofExercisesPostAsync(new Get_API_Url().GetScheduleListApi(_baseUrl, Search), true, _objHeaderModel, _objExerciseListingRequest);
                var result = _objExerciseListingResponse.Response;
                if (result.StatusCode == 200)
                {
                    if (result.workoutlist.Count > 0)
                    {
                        WorkoutList.ItemsSource = result.workoutlist;
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show("No Data!!");
                    }
                }
                else{
                    await Navigation.PopAllPopupAsync();
                    await DisplayAlert("Message", "No record found", "OK");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private void WorkoutList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var obj = ((ItemTappedEventArgs)e).Item as Workoutlist;
            var WorkoutId = obj.WorkoutId;
            if (WorkoutId > 0)
            {
                App.NavigationPage.Navigation.PushAsync(new WorkoutDetailsPage(obj));
            }
            else
            {
                DependencyService.Get<IToast>().Show("Error!");
            }
        }

        private void BtnNewUnscheduledWorkout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddUnScheduledWorkoutPage());
        }

        private async void XFDDWorkoutFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var FilterTypedata = picker.SelectedItem as FilterUser;
                Search = FilterTypedata.ListName;
                LoadWorkoutListData();
            }
        }
    }
}