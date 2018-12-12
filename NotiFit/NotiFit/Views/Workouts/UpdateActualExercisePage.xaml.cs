using AsNum.XFControls.Services;
using Notifit.Services.ApiHandler;
using Notifit.Services.Models;
using Notifit.Services.Models.RequestModels;
using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargetTransport.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Workouts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateActualExercisePage : ContentPage
	{
        #region Variable Declaration
        private Frame frame4txtbx1;
        private Frame frame4txtbx2;
        private StackLayout stackLayout;
        private Entry entry1;
        private Entry entry2;
        private Label label4txtbx1;
        private Label label4txtbx2;
        private Label label;
        private WorkoutTypesViewModel _objworkoutTypesViewModel;
        int? NumberofSets;
        private long WorkOutType;
        private int Workoutid;
        private AddActualExerciseRequest _objAddActualExerciseRequest;
       private AddActualExerciseResponse _objAddActualExerciseResponse;
        private RestApi _apiService;
        private EditExercise _objEditExercise;
        private string _baseUrl;
        private HeaderModel _objHeaderModel;
        #endregion
        public UpdateActualExercisePage (EditExercise ObjEditExercise)
		{
			InitializeComponent ();
            _objworkoutTypesViewModel = new WorkoutTypesViewModel();
            dropdownExerciseType.ItemsSource = _objworkoutTypesViewModel.WorkOutSource;
            _objEditExercise = new EditExercise();
            _objEditExercise = ObjEditExercise;
            WorkOutType = _objEditExercise.ExerciseTypeId;
            NumberofSets = _objEditExercise.SetsNumber;
            Workoutid = Convert.ToInt32(_objEditExercise.WorkOutId);
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            _baseUrl = Settings.Url + Domain.AddActualExerciseApiConstant;
            _objAddActualExerciseRequest = new AddActualExerciseRequest();
            _objAddActualExerciseResponse = new AddActualExerciseResponse();
            LoadPageView();
        }

        public void LoadPageView()
        {
            XFTxtboxExerciseName.Text = _objEditExercise.ExerciseName;
            EntryNoOfSets.Text = _objEditExercise.SetsNumber.ToString();
            dropdownExerciseType.SelectedIndex = Convert.ToInt32(WorkOutType - 1);
            
            if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps_with_weight))
            { 

                for (int i = 1; i <= NumberofSets; i++)
                {
                    stackLayout = new StackLayout
                    {
                        Spacing = 10,
                        Orientation = StackOrientation.Horizontal
                    };
                    frame4txtbx1 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };

                    entry1 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric,
                        
                       
                    };
                    if (_objEditExercise.IsFillfromGoal)
                    {
                        entry1.Text = _objEditExercise.weightList[i - 1].TotalWeight.ToString();
                    }
                      

                    label4txtbx1 = new Label
                    {
                        Text = "kg",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#FF740E")
                    };
                    label = new Label
                    {
                        Text = "X",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.Gray,
                        FontSize = 20
                    };
                    frame4txtbx2 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };
                    entry2 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric,
                       
                    };
                    if (_objEditExercise.IsFillfromGoal)
                    {
                        entry2.Text = _objEditExercise.weightList[i - 1].TotalRaps.ToString();
                    }
                    
                    label4txtbx2 = new Label
                    {
                        Text = "reps",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#FF740E")
                    };


                    frame4txtbx1.Content = entry1;
                    frame4txtbx2.Content = entry2;

                    stackLayout.Children.Add(frame4txtbx1);
                    stackLayout.Children.Add(label4txtbx1);
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(frame4txtbx2);
                    stackLayout.Children.Add(label4txtbx2);
                    GridSets.Children.Add(stackLayout);
                }
            }
            if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps_with_Level))
            {

                for (int i = 1; i <= NumberofSets; i++)
                {
                    stackLayout = new StackLayout
                    {
                        Spacing = 10,
                        Orientation = StackOrientation.Horizontal
                    };
                    frame4txtbx1 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };

                    entry1 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric
                    };
                    if (_objEditExercise.IsFillfromGoal)
                    {
                        entry1.Text = _objEditExercise.levelList[i - 1].TotalWeight.ToString();
                    }
                    label4txtbx1 = new Label
                    {
                        Text = "kg",
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false,
                        TextColor = Color.FromHex("#FF740E")
                    };
                    label = new Label
                    {
                        Text = "X",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.Gray,
                        FontSize = 20
                    };
                    frame4txtbx2 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };
                    entry2 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric
                    };
                    if (_objEditExercise.IsFillfromGoal)
                    {
                        entry2.Text = _objEditExercise.levelList[i - 1].TotalRaps.ToString();
                    }
                    label4txtbx2 = new Label
                    {
                        Text = "reps",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#FF740E")
                    };


                    frame4txtbx1.Content = entry1;
                    frame4txtbx2.Content = entry2;

                    stackLayout.Children.Add(frame4txtbx1);
                    stackLayout.Children.Add(label4txtbx1);
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(frame4txtbx2);
                    stackLayout.Children.Add(label4txtbx2);
                    GridSets.Children.Add(stackLayout);
                }
            }
            if (WorkOutType == Convert.ToInt32(WorkoutSets.Timed_Sets_seconds))
            {

                for (int i = 1; i <= NumberofSets; i++)
                {
                    stackLayout = new StackLayout
                    {
                        Spacing = 10,
                        Orientation = StackOrientation.Horizontal
                    };
                    frame4txtbx1 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        IsVisible = false,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };

                    entry1 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric
                    };
                    
                    label4txtbx1 = new Label
                    {
                        Text = "kg",
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false,
                        TextColor = Color.FromHex("#FF740E")
                    };
                    label = new Label
                    {
                        Text = "X",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.Gray,
                        IsVisible = false,
                        FontSize = 20
                    };
                    frame4txtbx2 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };
                    entry2 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric
                    };
                    if (_objEditExercise.IsFillfromGoal)
                    {
                        entry2.Text = _objEditExercise.timeList[i - 1].TimedSet.ToString();
                    }
                    label4txtbx2 = new Label
                    {
                        Text = "seconds",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#FF740E")
                    };


                    frame4txtbx1.Content = entry1;
                    frame4txtbx2.Content = entry2;

                    stackLayout.Children.Add(frame4txtbx1);
                    stackLayout.Children.Add(label4txtbx1);
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(frame4txtbx2);
                    stackLayout.Children.Add(label4txtbx2);
                    GridSets.Margin = new Thickness(80, 0, 0, 0);
                    GridSets.Children.Add(stackLayout);
                }
            }
            if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps))
            {

                for (int i = 1; i <= NumberofSets; i++)
                {
                    stackLayout = new StackLayout
                    {
                        Spacing = 10,
                        Orientation = StackOrientation.Horizontal
                    };
                    frame4txtbx1 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        IsVisible = false,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };

                    entry1 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric
                    };

                    label4txtbx1 = new Label
                    {
                        Text = "kg",
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false,
                        TextColor = Color.FromHex("#FF740E")
                    };
                    label = new Label
                    {
                        Text = "X",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.Gray,
                        IsVisible = false,
                        FontSize = 20
                    };
                    frame4txtbx2 = new Frame
                    {
                        HeightRequest = -1,
                        Padding = new Thickness(0, 0, 0, 0),
                        BorderColor = Color.FromHex("#93CBF0"),
                        WidthRequest = 80,
                        BackgroundColor = Color.White,
                        CornerRadius = 0
                    };
                    entry2 = new Entry
                    {
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 80,
                        Keyboard = Keyboard.Numeric
                    };
                    if (_objEditExercise.IsFillfromGoal)
                    {
                        entry2.Text = _objEditExercise.respList[i - 1].RepsSets.ToString();
                    }
                    label4txtbx2 = new Label
                    {
                        Text = "seconds",
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#FF740E")
                    };


                    frame4txtbx1.Content = entry1;
                    frame4txtbx2.Content = entry2;

                    stackLayout.Children.Add(frame4txtbx1);
                    stackLayout.Children.Add(label4txtbx1);
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(frame4txtbx2);
                    stackLayout.Children.Add(label4txtbx2);
                    GridSets.Margin = new Thickness(80, 0, 0, 0);
                    GridSets.Children.Add(stackLayout);
                }
            }
            if (WorkOutType == Convert.ToInt32(WorkoutSets.Distance_x_Time))
            {

                //for (int i = 1; i <= NumberofSets; i++)
                //{
                stackLayout = new StackLayout
                {
                    Spacing = 10,
                    Orientation = StackOrientation.Horizontal
                };
                frame4txtbx1 = new Frame
                {
                    HeightRequest = -1,
                    Padding = new Thickness(0, 0, 0, 0),
                    BorderColor = Color.FromHex("#93CBF0"),
                    WidthRequest = 80,
                    
                    BackgroundColor = Color.White,
                    CornerRadius = 0
                };

                entry1 = new Entry
                {
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 80,
                    Keyboard = Keyboard.Numeric
                };
                if (_objEditExercise.IsFillfromGoal)
                {
                    entry1.Text = _objEditExercise.DistanceInKm.ToString();
                }
                label4txtbx1 = new Label
                {
                    Text = "kg",
                    VerticalOptions = LayoutOptions.Center,
                    IsVisible = false,
                    TextColor = Color.FromHex("#FF740E")
                };
                label = new Label
                {
                    Text = "in",
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.Gray,
                    IsVisible = true,
                    FontSize = 20
                };
                frame4txtbx2 = new Frame
                {
                    HeightRequest = -1,
                    Padding = new Thickness(0, 0, 0, 0),
                    BorderColor = Color.FromHex("#93CBF0"),
                    WidthRequest = 80,
                    BackgroundColor = Color.White,
                    CornerRadius = 0
                };
                entry2 = new Entry
                {
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 80,
                    Keyboard = Keyboard.Numeric
                };
                if (_objEditExercise.IsFillfromGoal)
                {
                    entry2.Text = _objEditExercise.distance.RepsSetsTime.ToString();
                }
                label4txtbx2 = new Label
                {
                    Text = "Minutes",
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromHex("#FF740E")
                };


                frame4txtbx1.Content = entry1;
                frame4txtbx2.Content = entry2;

                stackLayout.Children.Add(frame4txtbx1);
                stackLayout.Children.Add(label4txtbx1);
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(frame4txtbx2);
                stackLayout.Children.Add(label4txtbx2);
                GridSets.Margin = new Thickness(80, 0, 0, 0);
                GridSets.Children.Add(stackLayout);
            }
            if (WorkOutType == Convert.ToInt32(WorkoutSets.Free_Text))
            {

                //for (int i = 1; i <= NumberofSets; i++)
                //{
                stackLayout = new StackLayout
                {
                    Spacing = 10,
                    Orientation = StackOrientation.Horizontal
                };
                frame4txtbx1 = new Frame
                {
                    HeightRequest = -1,
                    Padding = new Thickness(0, 0, 0, 0),
                    BorderColor = Color.FromHex("#93CBF0"),
                    WidthRequest = 80,
                    IsVisible = false,
                    BackgroundColor = Color.White,
                    CornerRadius = 0
                };

                entry1 = new Entry
                {
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 80,
                    Keyboard = Keyboard.Numeric
                };

                label4txtbx1 = new Label
                {
                    Text = "kg",
                    VerticalOptions = LayoutOptions.Center,
                    IsVisible = false,
                    TextColor = Color.FromHex("#FF740E")
                };
                label = new Label
                {
                    Text = "in",
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.Gray,
                    IsVisible = false,
                    FontSize = 20
                };
                frame4txtbx2 = new Frame
                {
                    HeightRequest = -1,
                    Padding = new Thickness(0, 0, 0, 0),
                    BorderColor = Color.FromHex("#93CBF0"),
                    WidthRequest = 150,
                    BackgroundColor = Color.White,
                    CornerRadius = 0
                };
                entry2 = new Entry
                {
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 150,
                    Keyboard = Keyboard.Default
                };
                if (_objEditExercise.IsFillfromGoal)
                {
                    entry2.Text = _objEditExercise.text.Text.ToString();
                }
                label4txtbx2 = new Label
                {
                    Text = "Minutes",
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromHex("#FF740E"),
                    IsVisible = false

                };


                frame4txtbx1.Content = entry1;
                frame4txtbx2.Content = entry2;

                stackLayout.Children.Add(frame4txtbx1);
                stackLayout.Children.Add(label4txtbx1);
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(frame4txtbx2);
                stackLayout.Children.Add(label4txtbx2);
                GridSets.Margin = new Thickness(80, 0, 0, 0);
                GridSets.Children.Add(stackLayout);
            }
            // }
        }

        //private void EntryNoOfSets_Unfocused(object sender, FocusEventArgs e)
        //{
        //    NumberofSets = Convert.ToInt32(EntryNoOfSets.Text);
        //    if (WorkOutType > 0)
        //    {
        //        GridSets.Children.Clear();
        //    }
        //    LoadPageView();
        //}

        private async void BtnSubmitExercise_Clicked(object sender, EventArgs e)
        {
            try
            {
                _objAddActualExerciseRequest.UserId = Convert.ToInt32(Settings.UserID);
                _objAddActualExerciseRequest.WorkOutId = Workoutid;
                _objAddActualExerciseRequest.UpdateList = new List<UpdateList>();
                _objHeaderModel.TokenCode = Settings.TokenCode;
                ActualRapList rl = new ActualRapList();
                UpdateList ml = new UpdateList();
                ActualWeightList wl = new ActualWeightList();
                LevList ll = new LevList();
                TimList tl = new TimList();
                DisList dl = new DisList();
                TextListOfActual atl = new TextListOfActual();

                ml.SetsNumber = Convert.ToInt32(EntryNoOfSets.Text);
                ml.ExerciseName = XFTxtboxExerciseName.Text;
                ml.ExerciseTypeId = Convert.ToInt32(WorkOutType);
                if(wl != null)
                {
                    ml.ImperialType = Settings.WeightName;
                }
                else{
                    ml.ImperialType = Settings.DistanceName;
                }
                foreach (StackLayout child in GridSets.Children)
                {
                    Entry entryWeight = (Entry)((Frame)child.Children[0]).Content;
                    string valueWeight = entryWeight.Text;

                    Entry rep = (Entry)((Frame)child.Children[3]).Content;
                    string valueRep = rep.Text;
                    if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps_with_weight))
                    {
                        wl.TotalRaps = Convert.ToInt32(valueRep);
                        wl.TotalWeight = Convert.ToDecimal(valueWeight);
                        wl.ImperialType = Settings.WeightName;
                        ml.weightList.Add(wl);
                        _objAddActualExerciseRequest.UpdateList.Add(ml);
                    }
                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps_with_Level))
                    {
                        ll.TotalRaps = Convert.ToInt32(valueRep);
                        ll.TotalWeight = Convert.ToDecimal(valueWeight);
                        ll.ImperialType = Settings.WeightName;
                        ml.levList.Add(ll);
                        _objAddActualExerciseRequest.UpdateList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Timed_Sets_seconds))
                    {
                        tl.TimedSet = Convert.ToInt32(valueRep);
                        ml.timList.Add(tl);
                        _objAddActualExerciseRequest.UpdateList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps))
                    {
                        rl.RepsSets = Convert.ToInt32(valueRep);
                        ml.rapList.Add(rl);
                        _objAddActualExerciseRequest.UpdateList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Distance_x_Time))
                    {
                        dl.RepsSetsTime = Convert.ToInt32(valueRep);
                        ml.DistanceInKm = Convert.ToDouble(valueWeight);
                        ml.disList.Add(dl);
                        _objAddActualExerciseRequest.UpdateList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Free_Text))
                    {
                        atl.Text = (valueRep);
                        ml.Text = valueRep;
                        ml.textListOfActual.Add(atl);
                        _objAddActualExerciseRequest.UpdateList.Add(ml);
                    }
                }
                _objAddActualExerciseResponse = await _apiService.AddActualExerciseAsync(new Get_API_Url().CommonBaseApi(_baseUrl), true, _objHeaderModel, _objAddActualExerciseRequest);
                if (_objAddActualExerciseResponse.Response.StatusCode == 200)
                {
                    DependencyService.Get<IToast>().Show(_objAddActualExerciseResponse.Response.Message);

                }

                //if (_objAddWorkoutResponse.StatusCode == 200)
                //{
                //    DependencyService.Get<IToast>().Show(_objAddWorkoutResponse.Message);
                //    WorkoutId = _objAddWorkoutResponse.WorkoutId;
                //}
                //else
                //{
                //    DependencyService.Get<IToast>().Show(_objAddWorkoutResponse.Message);
                //}



            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

        }

        private void dropdownExerciseType_SelectedIndexChanged(object sender, EventArgs e)
        {
           // DependencyService.Get<IToast>().Show("Sorry Not Allowed Here!");
            //var picker = (Picker)sender;
            //int selectedIndex = picker.SelectedIndex;

            //if (selectedIndex != -1)
            //{
            //    var data = picker.Items[picker.SelectedIndex];
            //    var WorkoutTypedata = picker.SelectedItem as WorkoutTypesEntity;
            //   // WorkOutType = WorkoutTypedata.TypeID;

            //}
            //else
            //{
            //    DependencyService.Get<IToast>().Show("please select Type!");
            //}
        }

        private void Cancel_Clicked(object sender,EventArgs e)
        {
            App.NavigationPage.Navigation.PopAsync(true);
        }

        
    }
}