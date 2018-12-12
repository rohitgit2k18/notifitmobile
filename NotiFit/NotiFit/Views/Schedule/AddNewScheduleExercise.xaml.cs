using AsNum.XFControls.Services;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewScheduleExercise : ContentPage
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
        #endregion
        public AddNewScheduleExercise ()
		{
			InitializeComponent ();
            _objworkoutTypesViewModel = new WorkoutTypesViewModel();
            dropdownExerciseType.ItemsSource = _objworkoutTypesViewModel.WorkOutSource;

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
                        Text = Settings.WeightName,
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false,
                        TextColor = Color.FromHex("#FF740E")
                    };
                    label = new Label
                    {
                        Text = "X",
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false,
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
                        Text = Settings.WeightName,
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
                        Text = Settings.WeightName,
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

        private void BtnSubmitExercise_Clicked(object sender, EventArgs e)
        {
            foreach (StackLayout child in GridSets.Children)
            {
                Entry entryWeight = (Entry)((Frame)child.Children[0]).Content;
                string valueWeight = entryWeight.Text;

                Entry rep = (Entry)((Frame)child.Children[3]).Content;
                string valueRep = rep.Text;
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
        private void Reset_Excercise(object sender,EventArgs e)
        {
            EntryNoOfSets.Text = "";
            ExcerciseName.Text = "";
            dropdownExerciseType.ItemsSource = _objworkoutTypesViewModel.WorkOutSource;
        }
    }
}