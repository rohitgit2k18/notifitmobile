using AsNum.XFControls.Services;
using Newtonsoft.Json;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using NotiFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Workouts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkoutDetailsPage : ContentPage
	{
        #region variable Declaration
        private Workoutlist _objWorkoutlist;
        private WorkOutDetailRequest _objWorkOutDetailRequest;
        private WorkOutDetailResponse _objWorkOutDetailResponse;
        private ExerciseDetailsByWorkoutIDResponse _objExerciseDetailsByWorkoutIDResponse;
        private RestApi _apiService;
        private string _baseUrl;
        private string _baseUrlDetailByID;
        private HeaderModel _objHeaderModel;
        public int WorkOutId { get; set; }
        #endregion
        public WorkoutDetailsPage( Workoutlist objWorkoutlist)
		{

            InitializeComponent ();
            _objWorkoutlist = new Workoutlist();
            _objWorkoutlist = objWorkoutlist;
            _objWorkOutDetailResponse = new WorkOutDetailResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.WorkoutDetailspiConstant;
            _baseUrlDetailByID = Settings.Url + Domain.ExerciseDetailsByIDApiConstant;
            _objExerciseDetailsByWorkoutIDResponse = new ExerciseDetailsByWorkoutIDResponse();
            LoadPageDetailsData();
            Settings.StatusName = objWorkoutlist.Status;
            WorkOutId = objWorkoutlist.WorkoutId;
            SetInProgress();
        }
        private async void SetInProgress()
        {
            try
            {
                //var userId = userID;
                //var pushtoken = pushToken;
                Common common = new Common();
                common.UserID = 1;
                SendUDIDEntityResponse _response = new SendUDIDEntityResponse();
                _response.Response = new Responses();

                    string s = JsonConvert.SerializeObject(common);
                    HttpResponseMessage response = null;
                    string uri = "http://noti.fit:130/api/Workout/StatusInProgress?WorkoutId=" + WorkOutId;
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
                        }
                        else
                        {
                            var ErrorResponse = await response.Content.ReadAsStringAsync();
                            _response.Response.Message = JsonConvert.DeserializeObject<string>(ErrorResponse);
                            DependencyService.Get<IToast>().Show("Token error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
        }

        private async void LoadPageDetailsData()
        {
            try
            {
                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objWorkOutDetailRequest = new WorkOutDetailRequest
                {
                    WorkoutId = _objWorkoutlist.WorkoutId
                };
                _objWorkOutDetailResponse = await _apiService.WorkoutDetailsPostAsync(new Get_API_Url().CommonBaseApi(_baseUrl), true, _objHeaderModel, _objWorkOutDetailRequest);
                var result = _objWorkOutDetailResponse.Response;
                if (result.StatusCode == 200)
                {
                    this.BindingContext = _objWorkOutDetailResponse.Response;

                    _objExerciseDetailsByWorkoutIDResponse = await _apiService.GetAsyncData_GetApi(new Get_API_Url().ExerciseDetailByWorkoutIDApi(_baseUrlDetailByID, _objWorkoutlist.WorkoutId), true, _objHeaderModel, _objExerciseDetailsByWorkoutIDResponse);
                    var _results = _objExerciseDetailsByWorkoutIDResponse.Response;
                    if (_results.StatusCode == 200)
                    {
                        foreach (var item in _results.editExercise)
                        {
                           
                           // string concatRight = null;
                            if (item.ExerciseTypeId==1)
                            {
                                item.IsId1 = true;
                                item.IsId2 = false;
                                item.IsId3 = false;
                                item.IsId4 = false;
                                item.IsId5 = false;
                                item.IsId6 = false;
                                string concatLeft = null;
                                string ConcatActual = null;
                                foreach ( var Items in item.weightList)
                                {                                   
                                    concatLeft += Items.TotalWeight +" kg"+" x" +Items.TotalRaps+" reps"+ "\n";                                   
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
                                concatLeft4 += item.DistanceInKm + " km"+" in" + item.distance.RepsSetsTime + " minutes" + "\n";
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
                                        ConcatActual5 += ActFreeTextitem.Text  + "\n";
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
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private void dropdownExerciseType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnAddExercise_Tapped(object sender, EventArgs e)
        {

        }

        private void XFBtnExerciseList_Tapped(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new WorkoutExerciseDetailListing(_objExerciseDetailsByWorkoutIDResponse));
        }
        private async void Complete_workout_clicked(object sender,EventArgs e)
        {
            try
            {
                Common common = new Common();
                common.UserID = 1;
                UpdateStatusResponse _response = new UpdateStatusResponse();
                //_response.Response = new Responses();

                string s = JsonConvert.SerializeObject(common);
                HttpResponseMessage response = null;
                string uri = "http://noti.fit:130/api/Workout/StatusUpdateCompleted?WorkoutId=" + _objWorkoutlist.WorkoutId;
                using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
                {
                    string token = Settings.TokenCode;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    response = await client.PostAsync(uri, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var SucessResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<UpdateStatusResponse>(SucessResponse);
                        DependencyService.Get<IToast>().Show("Status Completed");
                    }
                    else
                    {
                        var ErrorResponse = await response.Content.ReadAsStringAsync();
                        _response = JsonConvert.DeserializeObject<UpdateStatusResponse>(ErrorResponse);
                        DependencyService.Get<IToast>().Show("Token error");
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
        }
    }
}