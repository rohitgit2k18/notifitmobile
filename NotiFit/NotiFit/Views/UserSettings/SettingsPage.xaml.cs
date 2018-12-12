using AsNum.XFControls.Services;
using NotiFit.Helpers;
using NotiFit.Models;
using NotiFit.ViewModels;
using NotiFit.Views.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.UserSettings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        private MeasurementViewModel _objmeasurementViewModel;
        private int WeightType;
        private int DistanceType;
		public SettingsPage ()
		{
			InitializeComponent ();
            _objmeasurementViewModel = new MeasurementViewModel();
            dropdownWeight.ItemsSource = _objmeasurementViewModel.measurementWightEntities;
            dropdownHeight.ItemsSource = _objmeasurementViewModel.measurementDistance;
            if(Settings.DistanceId > 0 && Settings.DistanceName!= null)
            {
                dropdownHeight.SelectedIndex = Settings.DistanceId - 1;
            }
            if(Settings.WeightId > 0 && Settings.WeightName != null)
            {
                dropdownWeight.SelectedIndex = Settings.WeightId - 1;
            }
        }

        private void xfBtnProfile_Clicked(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new UserProfilePage());
        }

        public static SettingsPage settingsPage;
        private void xfBtnNotification_Clicked(object sender, EventArgs e)
        {
            settingsPage = this;
            App.NavigationPage.Navigation.PushAsync(new MyNotificationPage());
        }
        private void xfBtnFrndNotification_Clicked(object sender,EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new FriendSees());
        }

        public void ShowPaymentStatus(bool result)
        {
            DisplayAlert("Payment Successfull", "Thank You! Your payment is successfully completed", "Ok");
        }

        private void xfBtnAddNotification_Clicked(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new AddNotificationPage());
        }

        private void dropdownWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as MeasurementEntity;
                WeightType = WorkoutTypedata.Id;
                Settings.WeightId = WeightType;
                Settings.WeightName = WorkoutTypedata.Name;
                

            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }
        private void dropdownDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var WorkoutTypedata = picker.SelectedItem as MeasurementDistance;
                DistanceType = WorkoutTypedata.Id;
                Settings.DistanceId = DistanceType;
                Settings.DistanceName = WorkoutTypedata.DistanceName;
            }
            else
            {
                DependencyService.Get<IToast>().Show("please select Type!");
            }
        }
    }
}