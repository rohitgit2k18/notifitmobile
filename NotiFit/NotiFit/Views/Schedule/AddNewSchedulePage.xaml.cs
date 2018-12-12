using AsNum.XFControls;
using AsNum.XFControls.Services;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.ViewModels;
using NotiFit.Views.Workouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewSchedulePage : ContentPage
	{
        #region Variable Seclaration
        private AddWorkoutScheduleRequest _objAddWorkoutScheduleRequest;
        private AddWorkoutScheduleResponse _objAddWorkoutScheduleResponse;
        private RecereenceList _objRecereenceList;
        private RestApi _apiService;
        private string _baseUrl;
        private HeaderModel _objHeaderModel;        
        private int ReccerenceId;
        private bool IsMondaySelected;
        private bool IsTuesdaySelected;
        private bool IsWednesdaySelected;
        private bool IsThursdaySelected;
        private bool IsFridaySelected;
        private bool IsSaturdaySelected;
        private bool IsSundaySelected;
        private int WorkoutId;
        private int pageId;
        #endregion
        public AddNewSchedulePage ()
		{
			InitializeComponent ();
            _objAddWorkoutScheduleRequest = new AddWorkoutScheduleRequest();
            _objAddWorkoutScheduleResponse = new AddWorkoutScheduleResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.AddWorkoutScheduleApiConstant;
            _objRecereenceList = new RecereenceList();
            dropdownReccerenceType.ItemsSource = _objRecereenceList.RecerrenceSource;
            BindingContext = _objAddWorkoutScheduleRequest;
        }
        
        

        private void BtnAddExercise_Tapped(object sender, EventArgs e)
        {
            

            int Exertype = 2;
            if(Settings.WeightName != "null" || Settings.DistanceName != "null")
            {
                if (WorkoutId > 0)
                {
                    App.NavigationPage.Navigation.PushAsync(new AddNewExcercisePage(WorkoutId, Exertype));
                }
                else
                {
                    DisplayAlert("Reminder", "Please save the scheduled workout", "Ok");
                }
            }
            else
            {
                DisplayAlert("Reminder", "Please select Metrics on setting page", "Ok");
            }


        }

        private void XFBtnMonday_Clicked(object sender, EventArgs e)
        {
            if(IsMondaySelected)
            {
                XFBtnMonday.BackgroundColor = Color.FromHex("#64BAEB");
                IsMondaySelected = false;
                _objAddWorkoutScheduleRequest.Monday = false;
            }
            else
            {
                XFBtnMonday.BackgroundColor = Color.FromHex("#FF7C12");
                IsMondaySelected = true;
                _objAddWorkoutScheduleRequest.Monday = true;
            }
           
        }

        private void XFBtnTuesday_Clicked(object sender, EventArgs e)
        {
            if (IsTuesdaySelected)
            {
                XFBtnTuesday.BackgroundColor = Color.FromHex("#64BAEB");
                IsTuesdaySelected = false;
                _objAddWorkoutScheduleRequest.Tuesday = false;
            }
            else
            {
                XFBtnTuesday.BackgroundColor = Color.FromHex("#FF7C12");
                IsTuesdaySelected = true;
                _objAddWorkoutScheduleRequest.Tuesday = true;
            }
        }

        private void XFBtnWednesday_Clicked(object sender, EventArgs e)
        {
            if (IsWednesdaySelected)
            {
                XFBtnWednesday.BackgroundColor = Color.FromHex("#64BAEB");
                IsWednesdaySelected = false;
                _objAddWorkoutScheduleRequest.Wednesday = false;
            }
            else
            {
                XFBtnWednesday.BackgroundColor = Color.FromHex("#FF7C12");
                IsWednesdaySelected = true;
                _objAddWorkoutScheduleRequest.Wednesday = true;
            }
        }

        private void XFBtnThursaday_Clicked(object sender, EventArgs e)
        {
            if (IsThursdaySelected)
            {
                XFBtnThursaday.BackgroundColor = Color.FromHex("#64BAEB");
                IsThursdaySelected = false;
                _objAddWorkoutScheduleRequest.Thursday = false;
            }
            else
            {
                XFBtnThursaday.BackgroundColor = Color.FromHex("#FF7C12");
                IsThursdaySelected = true;
                _objAddWorkoutScheduleRequest.Thursday = true;
            }
        }

        private void XFBtnFriday_Clicked(object sender, EventArgs e)
        {
            if (IsFridaySelected)
            {
                XFBtnFriday.BackgroundColor = Color.FromHex("#64BAEB");
                IsFridaySelected = false;
                _objAddWorkoutScheduleRequest.Friday = false;
            }
            else
            {
                XFBtnFriday.BackgroundColor = Color.FromHex("#FF7C12");
                IsFridaySelected = true;
                _objAddWorkoutScheduleRequest.Friday = true;
            }
        }

        private void XFBtnSaturday_Clicked(object sender, EventArgs e)
        {
            if (IsSaturdaySelected)
            {
                XFBtnSaturday.BackgroundColor = Color.FromHex("#64BAEB");
                IsSaturdaySelected = false;
                _objAddWorkoutScheduleRequest.Saturday = false;
            }
            else
            {
                XFBtnSaturday.BackgroundColor = Color.FromHex("#FF7C12");
                IsSaturdaySelected = true;
                _objAddWorkoutScheduleRequest.Saturday = true;
            }
        }

        private void XFBtnSunday_Clicked(object sender, EventArgs e)
        {
            if (IsSundaySelected)
            {
                XFBtnSunday.BackgroundColor = Color.FromHex("#64BAEB");
                IsSundaySelected = false;
                _objAddWorkoutScheduleRequest.Sunday = false;
            }
            else
            {
                XFBtnSunday.BackgroundColor = Color.FromHex("#FF7C12");
                IsSundaySelected = true;
                _objAddWorkoutScheduleRequest.Sunday = true;
            }
        }

        private void dropdownReccerenceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var ReccerenceTypedata = picker.SelectedItem as RecerrenceListEntity;
                 ReccerenceId = ReccerenceTypedata.RecerrenceID;

            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }

        private void CheckboxTextme_CheckChanged(object sender, EventArgs e)
        {
            var IsChecked = ((CheckBox)sender).Checked;
            if(IsChecked)
            {
                XFEntryTxtMe.IsEnabled = true;
            }
            else
            {
                XFEntryTxtMe.IsEnabled = false;
            }
        }

        private void checkboxTextfeirnd_CheckChanged(object sender, EventArgs e)
        {
            var IsChecked = ((CheckBox)sender).Checked;
            if (IsChecked)
            {
                XFEntryTxtFriend.IsEnabled = true;
            }
            else
            {
                XFEntryTxtFriend.IsEnabled = false;
            }
        }

      

        private async void XFSubmitButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var TextMe = "";
                if(string.IsNullOrEmpty(XFEntryTxtMe.Text))
                {
                    TextMe = "00:00:00";
                }
                else{
                    TextMe = "00:" + XFEntryTxtMe.Text;
                }
                var TextFrind = "";
                if (string.IsNullOrEmpty(XFEntryTxtFriend.Text))
                {
                    TextFrind = "00:00:00";
                }
                else
                {
                    TextFrind = "00:" + XFEntryTxtFriend.Text;
                }


                _objHeaderModel = new HeaderModel();
                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objAddWorkoutScheduleRequest.ScheduleTime = XfTPStartTime.Time;
                _objAddWorkoutScheduleRequest.StartDate = XFDPstartDate.Date;
                _objAddWorkoutScheduleRequest.EndDate = XFDPEndDate.Date;
                _objAddWorkoutScheduleRequest.RecurrenceId = ReccerenceId;
                if(!string.IsNullOrEmpty(TextMe))
                {
                 _objAddWorkoutScheduleRequest.TextMeTime =TimeSpan.Parse(TextMe);
                }
                if (!string.IsNullOrEmpty(TextFrind))
                {
                    _objAddWorkoutScheduleRequest.TextFriendTime = TimeSpan.Parse(TextFrind);
                }


                if (_objAddWorkoutScheduleRequest.StartDate == null || _objAddWorkoutScheduleRequest.RecurrenceId <= 0
                    || _objAddWorkoutScheduleRequest.ScheduleTime == null || string.IsNullOrEmpty(_objAddWorkoutScheduleRequest.WorkoutText))
                {
                    DependencyService.Get<IToast>().Show("please fill the form completelty!");
                }
                else
                {
                                                                                                                                                                       
                    _objAddWorkoutScheduleResponse = await _apiService.AddScheduleDetailAsync(new Get_API_Url().CommonBaseApi(_baseUrl), true, _objHeaderModel, _objAddWorkoutScheduleRequest);

                    if (_objAddWorkoutScheduleResponse.StatusCode == 200)
                    {
                        DependencyService.Get<IToast>().Show(_objAddWorkoutScheduleResponse.Message);
                        WorkoutId = _objAddWorkoutScheduleResponse.WorkoutId;
                        pageId = 2;
                        XFBtnSave.IsEnabled = false;
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show(_objAddWorkoutScheduleResponse.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private void Discard_Changes(object sender, EventArgs e)
        {
            _objAddWorkoutScheduleRequest = new AddWorkoutScheduleRequest();
            _objAddWorkoutScheduleResponse = new AddWorkoutScheduleResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.AddWorkoutScheduleApiConstant;
            _objRecereenceList = new RecereenceList();
            dropdownReccerenceType.ItemsSource = _objRecereenceList.RecerrenceSource;
            BindingContext = _objAddWorkoutScheduleRequest;
            XFEntryTxtFriend.Text = "";
            XFEntryTxtMe.Text = "";
            XfTPStartTime.Time = TimeSpan.Parse("00:00");
            XFBtnMonday.BackgroundColor = Color.FromHex("#64BAEB");
            IsMondaySelected = false;
            _objAddWorkoutScheduleRequest.Monday = false;
            XFBtnTuesday.BackgroundColor = Color.FromHex("#64BAEB");
            IsTuesdaySelected = false;
            _objAddWorkoutScheduleRequest.Tuesday = false;
            XFBtnWednesday.BackgroundColor = Color.FromHex("#64BAEB");
            IsWednesdaySelected = false;
            _objAddWorkoutScheduleRequest.Wednesday = false;
            XFBtnThursaday.BackgroundColor = Color.FromHex("#64BAEB");
            IsThursdaySelected = false;
            _objAddWorkoutScheduleRequest.Thursday = false;
            XFBtnFriday.BackgroundColor = Color.FromHex("#64BAEB");
            IsFridaySelected = false;
            _objAddWorkoutScheduleRequest.Friday = false;
            XFBtnSaturday.BackgroundColor = Color.FromHex("#64BAEB");
            IsSaturdaySelected = false;
            _objAddWorkoutScheduleRequest.Saturday = false;
            XFBtnSunday.BackgroundColor = Color.FromHex("#64BAEB");
            IsSundaySelected = false;
            _objAddWorkoutScheduleRequest.Sunday = false;
            
        }
    }
}