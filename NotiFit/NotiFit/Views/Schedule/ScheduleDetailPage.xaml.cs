using AsNum.XFControls.Services;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using NotiFit.ViewModels;
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
	public partial class ScheduleDetailPage : ContentPage
	{
        #region variable Declaration   
        private ScheduleDetailsRequest _objScheduleDetailsRequest;
        private RecereenceList _objRecereenceList;
        private ScheduleDetailsResponse _objScheduleDetailsResponse;
        private ExerciseScheduleDetailRequest _objExerciseScheduleDetailRequest;
        private ExerciseDetailsByWorkoutIDResponse _objExerciseDetailsByWorkoutIDResponse;
        private RestApi _apiService;
        private string _baseUrl;
        private string _baseUrlDetailByID;
        private HeaderModel _objHeaderModel;
        private int ScheduleIds;
        #endregion
        public ScheduleDetailPage ( int ScheduleId)
		{
			InitializeComponent ();
            ScheduleIds = ScheduleId;
            _objScheduleDetailsRequest = new ScheduleDetailsRequest();
            _objScheduleDetailsResponse = new ScheduleDetailsResponse();
            _objExerciseScheduleDetailRequest = new ExerciseScheduleDetailRequest();
            _objExerciseDetailsByWorkoutIDResponse = new ExerciseDetailsByWorkoutIDResponse();
            _apiService = new RestApi();
            _objRecereenceList = new RecereenceList();
            dropdownReccerenceType.ItemsSource = _objRecereenceList.RecerrenceSource;
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.ScheduleDetailsApiConstant;
            _baseUrlDetailByID = Settings.Url + Domain.ExerciseScheduleDetailsApiConstant;
            LoadScheduleDetails();
        }
        private async void LoadScheduleDetails()
        {
            try
            {
                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objScheduleDetailsRequest = new ScheduleDetailsRequest
                {
                    ScheduleId = ScheduleIds
                };
                _objScheduleDetailsResponse = await _apiService.ScheduleDetailsAsync(new Get_API_Url().ScheduleDetailsApi(_baseUrl, _objScheduleDetailsRequest.ScheduleId), true, _objHeaderModel, _objScheduleDetailsRequest);
                var result = _objScheduleDetailsResponse.Response;
                if (result.StatusCode == 200)
                {
                    if (_objScheduleDetailsResponse.Response.deatils.TextMeTime > 0)
                    {
                        CheckboxTextme.Checked = true;
                    }
                    if(_objScheduleDetailsResponse.Response.deatils.TextFriendTime > 0)
                    {
                        checkboxTextfeirnd.Checked = true;
                    }
                    var selectedId = _objScheduleDetailsResponse.Response.deatils.RecurrenceId;
                    if (selectedId > 0)
                    {
                        dropdownReccerenceType.SelectedIndex = selectedId - 1;
                    }
                    this.BindingContext = _objScheduleDetailsResponse.Response.deatils;

                    _objExerciseDetailsByWorkoutIDResponse = await _apiService.GetAsyncData_GetApi(new Get_API_Url().ExerciseScheduleDetailsApi(_baseUrlDetailByID, _objScheduleDetailsRequest.ScheduleId), true, _objHeaderModel, _objExerciseDetailsByWorkoutIDResponse);
                    var _results = _objExerciseDetailsByWorkoutIDResponse.Response;
                    if (_results.StatusCode == 200)
                    {
                        foreach (var item in _results.editExercise)
                        {

                            // string concatRight = null;
                            if (item.ExerciseTypeId == 1)
                            {
                                item.IsId1 = true;
                                item.IsId2 = false;
                                item.IsId3 = false;
                                item.IsId4 = false;
                                item.IsId5 = false;
                                item.IsId6 = false;
                                string concatLeft = null;
                                string ConcatActual = null;
                                foreach (var Items in item.weightList)
                                {
                                    concatLeft += Items.TotalWeight + " kg" + " x" + Items.TotalRaps + " reps" + "\n";
                                }
                                item.SetsDescription = concatLeft;
                                foreach (var actualitem in item.actualWeightModel)
                                {
                                    if (item.actualWeightModel.Count > 0)
                                    {
                                        ConcatActual += actualitem.TotalWeight + " kg" + " x" + actualitem.TotalRaps + " reps" + "\n";
                                    }
                                }
                                item.ActualSetsDescription = ConcatActual;
                                //concatTabName += item. + "\n";
                                //concatTabTime += value.Repetaion + "\n";
                            }
                            if (item.ExerciseTypeId == 2)
                            {
                                item.IsId2 = true;
                                item.IsId3 = false;
                                item.IsId4 = false;
                                item.IsId5 = false;
                                item.IsId6 = false;
                                string concatLeft1 = null;
                                string ConcatActual1 = null;
                                foreach (var Items1 in item.levelList)
                                {
                                    concatLeft1 += Items1.TotalWeight + " x" + Items1.TotalRaps + " reps" + "\n";
                                }
                                item.SetsDescription = concatLeft1;
                                foreach (var actualitem1 in item.actualLevelList)
                                {
                                    if (item.actualLevelList.Count > 0)
                                    {
                                        ConcatActual1 += actualitem1.TotalWeight + " x" + actualitem1.TotalRaps + " reps" + "\n";
                                    }
                                }
                                item.ActualSetsDescription = ConcatActual1;
                            }
                            if (item.ExerciseTypeId == 3)
                            {
                                item.IsId1 = false;
                                item.IsId2 = false;
                                item.IsId3 = true;
                                item.IsId4 = false;
                                item.IsId5 = false;
                                item.IsId6 = false;
                                string concatLeft2 = null;
                                string ConcatActual2 = null;
                                foreach (var Items2 in item.timeList)
                                {
                                    concatLeft2 += Items2.TimedSet + "Seconds" + "\n";
                                }
                                item.SetsDescription = concatLeft2;
                                foreach (var actualitem2 in item.actualTimeList)
                                {
                                    if (item.actualTimeList.Count > 0)
                                    {
                                        ConcatActual2 += actualitem2.TimedSet + "Seconds" + "\n";
                                    }
                                }
                                item.ActualSetsDescription = ConcatActual2;
                            }
                            if (item.ExerciseTypeId == 4)
                            {
                                item.IsId1 = false;
                                item.IsId2 = false;
                                item.IsId3 = false;
                                item.IsId4 = true;
                                item.IsId5 = false;
                                item.IsId6 = false;
                                string concatLeft3 = null;
                                string ConcatActual3 = null;
                                foreach (var Items3 in item.respList)
                                {
                                    concatLeft3 += Items3.RepsSets + "reps" + "\n";
                                }
                                item.SetsDescription = concatLeft3;
                                foreach (var actualitem3 in item.actualRapsList)
                                {
                                    if (item.actualRapsList.Count > 0)
                                    {
                                        ConcatActual3 += actualitem3.RepsSets + "reps" + "\n";
                                    }
                                }
                                item.ActualSetsDescription = ConcatActual3;
                            }
                            if (item.ExerciseTypeId == 5)
                            {
                                item.IsId1 = false;
                                item.IsId2 = false;
                                item.IsId3 = false;
                                item.IsId4 = false;
                                item.IsId5 = true;
                                item.IsId6 = false;
                                string concatLeft4 = null;
                                string ConcatActual4 = null;
                                concatLeft4 += item.DistanceInKm + " km" + " in" + item.distance.RepsSetsTime + " minutes" + "\n";
                                item.SetsDescription = concatLeft4;
                                foreach (var ActDisitem in item.actualDistance)
                                {
                                    if (item.actualDistance.Count > 0)
                                    {
                                        ConcatActual4 += item.ActualDistanceInKm + " km" + " in" + ActDisitem.RepsSetsTime + " minutes" + "\n";
                                    }

                                }

                                item.ActualSetsDescription = concatLeft4;
                            }
                            if (item.ExerciseTypeId == 6)
                            {
                                item.IsId1 = false;
                                item.IsId2 = false;
                                item.IsId3 = false;
                                item.IsId4 = false;
                                item.IsId5 = false;
                                item.IsId6 = true;
                                string concatLeft5 = null;
                                string ConcatActual5 = null;
                                concatLeft5 += item.text.Text + "\n";
                                item.SetsDescription = concatLeft5;
                                foreach (var ActFreeTextitem in item.actualText)
                                {
                                    if (item.actualText.Count > 0)
                                    {
                                        ConcatActual5 += ActFreeTextitem.Text + "\n";
                                    }

                                }

                                item.ActualSetsDescription = ConcatActual5;
                            }
                        }

                        // ExerciseList.ItemsSource = _results.editExercise;
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show("Error!!");
                    }
                }
                else
                {
                    DependencyService.Get<IToast>().Show("Error!!");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private void dropdownReccerenceType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CheckboxTextme_CheckChanged(object sender, EventArgs e)
        {

        }

        private void checkboxTextfeirnd_CheckChanged(object sender, EventArgs e)
        {

        }

        private void XFSubmitButton_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnAddExercise_Tapped(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new ScheduleGoalListing(_objExerciseDetailsByWorkoutIDResponse));
        }
    }
}