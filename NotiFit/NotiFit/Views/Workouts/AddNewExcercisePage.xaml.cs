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
    public partial class AddNewExcercisePage : ContentPage
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
        int NumberofSets;
        private int WorkOutType;
        private Int64 Workoutid;
        private AddExerciseWorkoutRequest _objAddExerciseWorkoutRequest;
        private AddExerciseWorkoutResponse _objAddExerciseWorkoutResponse;
        private RestApi _apiService;
        private string _baseUrl;
        private HeaderModel _objHeaderModel;
        private int pageId;
        private List<string> names = new List<string>
        {
            "Squat","Leg press","Lunge","Deadlift","Leg extension","Leg curl","Standing calf raise","Seated calf raise","Hip adductor","Bench press","Chest fly","Push-up","Pull-down","Pull-up","Bent-over row",
    "Upright row",
    "Shoulder press",
    "Shoulder fly",
    "Lateral raise",
    "Shoulder shrug"
        };
        #endregion
        public AddNewExcercisePage(Int64 WkoutId,int Exertype)
        {
            InitializeComponent();
            _objworkoutTypesViewModel = new WorkoutTypesViewModel();
            dropdownExerciseType.ItemsSource = _objworkoutTypesViewModel.WorkOutSource;
            Workoutid = WkoutId;
            _objAddExerciseWorkoutRequest = new AddExerciseWorkoutRequest();
            _objAddExerciseWorkoutResponse = new AddExerciseWorkoutResponse();
            _apiService = new RestApi();
            _objHeaderModel = new HeaderModel();
            if(Exertype==1)
            {
                _baseUrl = Settings.Url + Domain.AddExerciseWorkoutApiConstant;
                pageId = 1;
            }
            if (Exertype == 2)
            {
                _baseUrl = Settings.Url + Domain.AddScheduleExerciseApiConstant;
                pageId = 2;
            }
            NamesListView.ItemsSource = names;
            
        }
        public void LoadPageView()
        {

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
                        Keyboard = Keyboard.Numeric
                    };

                    label4txtbx1 = new Label
                    {
                        Text = Settings.WeightName,
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
                        Keyboard = Keyboard.Numeric
                    };
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
                    Text = Settings.DistanceName,
                    VerticalOptions = LayoutOptions.Center,
                    IsVisible = true,
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

        private void EntryNoOfSets_Unfocused(object sender, FocusEventArgs e)
        {
            NumberofSets = Convert.ToInt32(EntryNoOfSets.Text);
            if (WorkOutType > 0)
            {
                GridSets.Children.Clear();
            }
            LoadPageView();
        }

        private async void BtnSubmitExercise_Clicked(object sender, EventArgs e)
        {
            try
            {
                _objAddExerciseWorkoutRequest.UserId = Convert.ToInt32(Settings.UserID);
                _objAddExerciseWorkoutRequest.WorkOutId = Convert.ToInt32(Workoutid);
                _objAddExerciseWorkoutRequest.mainList = new List<MainList>();
                _objHeaderModel.TokenCode = Settings.TokenCode;
                RapList rl = new RapList();
                MainList ml = new MainList();
                WeightList wl = new WeightList();
                LevelList ll = new LevelList();
                TimedList tl = new TimedList();


                ml.SetsNumber = NumberofSets;
                ml.ExerciseName = XFTxtboxExerciseName.Text;
                ml.ExerciseTypeId = WorkOutType;                
                if (Settings.WeightName != null)
                {
                    ml.ImperialType = "Weight";
                }
                else
                {
                    ml.ImperialType = "Distance";
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
                        wl.TotalWeight = Convert.ToInt32(valueWeight);
                        wl.ImperialType = Settings.WeightName;
                        ml.weightList.Add(wl);
                        _objAddExerciseWorkoutRequest.mainList.Add(ml);
                    }
                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps_with_Level))
                    {
                        ll.TotalRaps = Convert.ToInt32(valueRep);
                        ll.TotalWeight = Convert.ToInt32(valueWeight);
                        ll.ImperialType = Settings.DistanceName;
                        ml.levelList.Add(ll);
                        _objAddExerciseWorkoutRequest.mainList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Timed_Sets_seconds))
                    {
                        tl.TimedSet = Convert.ToInt32(valueRep);
                        ml.timedList.Add(tl);
                        _objAddExerciseWorkoutRequest.mainList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Sets_x_Reps))
                    {
                        rl.RepsSets = Convert.ToInt32(valueRep);
                        ml.rapList.Add(rl);
                        _objAddExerciseWorkoutRequest.mainList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Distance_x_Time))
                    {
                        ml.distance.RepsSetsTime = Convert.ToInt32(valueRep);
                        ml.DistanceInKm = Convert.ToInt32(valueWeight);
                        _objAddExerciseWorkoutRequest.mainList.Add(ml);
                    }

                    else if (WorkOutType == Convert.ToInt32(WorkoutSets.Free_Text))
                    {
                        ml.textList.Text = (valueWeight);
                        ml.Text = valueWeight;
                        _objAddExerciseWorkoutRequest.mainList.Add(ml);
                    }
                }
               
                _objAddExerciseWorkoutResponse = await _apiService.AddExerciseWorkoutPostAsync(new Get_API_Url().CommonBaseApi(_baseUrl), true, _objHeaderModel, _objAddExerciseWorkoutRequest);
                if (_objAddExerciseWorkoutResponse.StatusCode == 200)
                {
                    
                    await Navigation.PopAsync(true);
                    //await Navigation.PopAsync(true);
                    DependencyService.Get<IToast>().Show(_objAddExerciseWorkoutResponse.Message);                    
                }
                else
                {
                    DependencyService.Get<IToast>().Show(_objAddExerciseWorkoutResponse.Message);
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
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as WorkoutTypesEntity;
                WorkOutType = WorkoutTypedata.TypeID;

            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }
        private void Cancel_Clicked(object sender,EventArgs e)
        {
            XFTxtboxExerciseName.Text = "";
            EntryNoOfSets.Text = "";           
            GridSets.Children.Clear();
            dropdownExerciseType.ItemsSource = _objworkoutTypesViewModel.WorkOutSource;
        }
        private void SearchBar_Pressed(object sender,EventArgs e)
        {
            
            var keyword = XFTxtboxExerciseName.Text;
            NamesListView.IsVisible = true;
            NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }

        private void Handle_ItemSelected(object sender,SelectedItemChangedEventArgs e)
        {
            var selecteditem = e.SelectedItem;
            if(selecteditem !=null)
            {
                XFTxtboxExerciseName.Text = selecteditem.ToString();
                NamesListView.IsVisible = false;
            }
            else
            {
                return;
            }
        }
    }
}