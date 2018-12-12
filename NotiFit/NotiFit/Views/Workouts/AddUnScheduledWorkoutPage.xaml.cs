using AsNum.XFControls;
using AsNum.XFControls.Services;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using System;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Workouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUnScheduledWorkoutPage : ContentPage
	{
        #region Variable Seclaration
        private AddWorkoutRequest _objAddWorkoutRequest;
        private AddWorkoutResponse _objAddWorkoutResponse;
        private RestApi _apiService;
        private string _baseUrl;
        private HeaderModel _objHeaderModel;
        private bool Autofinsh=false;
        private Int64 WorkoutId;
        #endregion
        public AddUnScheduledWorkoutPage ()
		{
			InitializeComponent ();
            _objAddWorkoutResponse = new AddWorkoutResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.AddWorkoutApiConstant;
            _objAddWorkoutRequest = new AddWorkoutRequest();
            BindingContext = _objAddWorkoutRequest;

        }

        private async void BtnAddExercise_Tapped(object sender, EventArgs e)
        {
            try
            {
                 
                if (Settings.WeightName != "null" || Settings.DistanceName != "null")
                { 
                     int Exertype = 1;
                    if (WorkoutId > 0)
                    {
                        await App.NavigationPage.Navigation.PushAsync(new AddNewExcercisePage(WorkoutId, Exertype));
                    }
                    else{
                        await DisplayAlert("Reminder", "Please save unscheduled workout", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Reminder", "Please select Metrics on setting page", "Ok");
                
                }

            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
            
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            var result = (CheckBox)sender;
            var _checked = result.Checked;
            if(_checked)
            {
                XftpFinishTime.IsEnabled = false;
                Autofinsh = true;
            }
            else
            {
                XftpFinishTime.IsEnabled = true;
                Autofinsh = false;
                _objAddWorkoutRequest.FinishTime = TimeSpan.Parse(XftpFinishTime.Time.ToString());
            }
        }

        private async void XFBtnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                _objAddWorkoutRequest.StartTime = TimeSpan.Parse(XfTPStartTime.Time.ToString());
                _objAddWorkoutRequest.FinishTime = TimeSpan.Parse(XftpFinishTime.Time.ToString());
                if (string.IsNullOrEmpty(_objAddWorkoutRequest.Description) ||
                  string.IsNullOrEmpty(_objAddWorkoutRequest.WorkoutNotes)
                  //string.IsNullOrEmpty(_objAddWorkoutRequest.StartTime)
                  )
                {
                    DependencyService.Get<IToast>().Show("Please fill all the required filed first!");
                }
                else
                {
                    _objHeaderModel.TokenCode = Settings.TokenCode;
                    _objAddWorkoutRequest.UserId = Convert.ToInt64(Settings.UserID);
                    _objAddWorkoutRequest.DateOfWorkout = XFDPstartDate.Date;
                    _objAddWorkoutRequest.AutoFinishTime = Autofinsh;
                    _objAddWorkoutResponse = await _apiService.AddWorkoutPostAsync(new Get_API_Url().CommonBaseApi(_baseUrl), true, _objHeaderModel, _objAddWorkoutRequest);

                    if (_objAddWorkoutResponse.StatusCode == 200)
                    {
                        WorkoutId = _objAddWorkoutResponse.WorkoutId;
                        DependencyService.Get<IToast>().Show(_objAddWorkoutResponse.Message);
                        XFBtnSave.IsEnabled = false;
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show(_objAddWorkoutResponse.Message);
                    }
                   
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private void XFBtnCancel_CLicked(object sender,EventArgs e)
        {
            _objAddWorkoutResponse = new AddWorkoutResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.AddWorkoutApiConstant;
            _objAddWorkoutRequest = new AddWorkoutRequest();
            BindingContext = _objAddWorkoutRequest;
            XfTPStartTime.Time = TimeSpan.Parse("00:00");
            XftpFinishTime.Time = TimeSpan.Parse("00:00");
        }
    }
}
