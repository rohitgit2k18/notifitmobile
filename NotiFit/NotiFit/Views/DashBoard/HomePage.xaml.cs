using AsNum.XFControls.Services;
using Com.OneSignal;
using Microcharts;
using Newtonsoft.Json;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.Views.Workouts;
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

namespace NotiFit.Views.DashBoard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        #region Variable Declaration
        private MissedDashBoardResponse _objMissedDashBoardResponse;
        private CompletedDasboardResponse _objCompletedDasboardResponse;
        private RestApi _apiService;
        private string _baseUrlMissed;
        private string _baseUrlCompleted;
        private HeaderModel _objHeaderModel;
        private int MissedWkot;
        private int CompltedWkot;
        List<Microcharts.Entry> entries;
        public int color { get; set; }
        #endregion

        
        public HomePage()
        {
            InitializeComponent();
            // following single line is used for setting the icon for IOS 
            NavigationPage.SetTitleIcon(this, "logo_white.png");
            _objMissedDashBoardResponse = new MissedDashBoardResponse();
            _objCompletedDasboardResponse = new CompletedDasboardResponse();
            _baseUrlMissed = Settings.Url + Domain.MissedCountApiConstant;
            _baseUrlCompleted = Settings.Url + Domain.CompltedCountApiConstant;
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            GetAvailableId();
            if (Settings.ProfilePicture == string.Empty)
            {
                imageProfile.Source = "profile.png";
            }
            else
            {
                imageProfile.Source = Settings.ProfilePicture;
            }
            LblUserName.Text = "Welcome," + Settings.Name;

        }
        void GetAvailableId()
        {
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }

        private async void IdsAvailable(string userID, string pushToken)
        {
            try
            {
                var userId = userID;
                var pushtoken = pushToken;
                Common common = new Common();
                common.UserID = 1;
                SendUDIDEntityResponse _response = new SendUDIDEntityResponse();
                _response.Response = new Responses();

                string s = JsonConvert.SerializeObject(common);
                HttpResponseMessage response = null;
                string uri = "http://noti.fit:130/api/PushNotification/UpdatePlayerId?PlayerId=" + userId;
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


        protected override void OnAppearing()
        {
            base.OnAppearing();            
            LoadDashboardPageData();
            if (color < 50)
            {
                //lessthan50.BackgroundColor = Color.LightGray;
                lessThan75Block.IsVisible = false;
                Above75Block.IsVisible = false;
            }
            if (color > 50 && color < 75)
            {
                //lessthan75.BackgroundColor = Color.LightGray;
                Lessthan50block.IsVisible = false;
                Above75Block.IsVisible = false;
            }
            if (color > 75)
            {
                Lessthan50block.IsVisible = false;
                lessThan75Block.IsVisible = false;
                //above75.BackgroundColor = Color.LightGray;
            }

        }
        private async void LoadDashboardPageData()
        {
            try
            {
                _objHeaderModel.TokenCode = Settings.TokenCode;
                _objMissedDashBoardResponse = await _apiService.GetAsyncData_GetApi(new Get_API_Url().CommonBaseApi(_baseUrlMissed), true, _objHeaderModel, _objMissedDashBoardResponse);
                if (_objMissedDashBoardResponse.Response.StatusCode == 200)
                {
                    MissedWkot = _objMissedDashBoardResponse.Response.compWorkout.Missed;

                    //_objCompletedDasboardResponse = await _apiService.GetAsyncData_GetApi(new Get_API_Url().CommonBaseApi(_baseUrlCompleted), true, _objHeaderModel, _objCompletedDasboardResponse);
                    //if (_objCompletedDasboardResponse.Response.StatusCode == 200)
                    //{
                    CompltedWkot = _objMissedDashBoardResponse.Response.compWorkout.Completed;
                        color = CompltedWkot;
                    //}
                }
                LoadGraphData();
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private void LoadGraphData()
        {
            entries = new List<Microcharts.Entry> {
            new Microcharts.Entry(0)
            {
                Label="",
                ValueLabel="",
                Color=SkiaSharp.SKColor.Parse("#ffffff")
            },
            new Microcharts.Entry(MissedWkot)
            {
                //Label="Missed",
                //TextColor=SkiaSharp.SKColor.Parse("#FF7D12"),
                //ValueLabel=MissedWkot.ToString(),
                Color=SkiaSharp.SKColor.Parse("#FF7D12")
                  
            },
            new Microcharts.Entry(CompltedWkot)
            {
                //Label="Completed",                
                 //TextColor=SkiaSharp.SKColor.Parse("#1895DA"),
                //ValueLabel=CompltedWkot.ToString(),
                Color=SkiaSharp.SKColor.Parse("#1895DA")
                   
            }
        };

            LblMissed.Text = MissedWkot.ToString() + " Missed";
            LblCompleted.Text = CompltedWkot.ToString() + " Completed";
            Chart1.Chart = new RadialGaugeChart
            {
                Entries = entries,
                MaxValue = 100
            };
        }
        private void GraphTapped_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkOutListingPage());
        }
        private void XFLblClickHere_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserSettings.SettingsPage());
        }
    }
}