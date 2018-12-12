using NotiFit.Helpers;
using NotiFit.Views.DashBoard;
using NotiFit.Views.Schedule;
using NotiFit.Views.Workouts;
using Plugin.Media.Abstractions;
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
	public partial class UserNavigationPage : MasterDetailPage
    {
        private MediaFile _mediaFile;
        public UserNavigationPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            if(Settings.ProfilePicture == string.Empty)
            {
                imgProfile.Source = "profile.png";
            }
            else
            {
                imgProfile.Source = Settings.ProfilePicture;
            }
            lblUserName.Text = Settings.Name;
        }

        private void GridDashBoard_Tapped(object sender, EventArgs e)
        {

            var detail = new NavigationPage(new HomePage());
            App.DetailPage = new HomePage();
            detail.Title = "HomePage";
            App.Navigation = detail.Navigation;
            Detail = detail;
            IsPresented = false;
        }

        private void GridWorkOut_Tapped(object sender, EventArgs e)
        {
            var detail = new NavigationPage(new WorkOutListingPage());
            App.DetailPage = new WorkOutListingPage();
            detail.Title = "WorkOutListing";
            App.Navigation = detail.Navigation;
            Detail = detail;
            IsPresented = false;
        }

        private void GridSchedule_Tapped(object sender, EventArgs e)
        {
            var detail = new NavigationPage(new ScheduleListingPage());
            App.DetailPage = new ScheduleListingPage();
            detail.Title = "ScheduleListing";
            App.Navigation = detail.Navigation;
            Detail = detail;
            IsPresented = false;
        }

        private void GridSettings_Tapped(object sender, EventArgs e)
        {
            var detail = new NavigationPage(new SettingsPage());
            App.DetailPage = new SettingsPage();
            detail.Title = "SettingsPage";
            App.Navigation = detail.Navigation;
            Detail = detail;
            IsPresented = false;
        }

        private void GridAboutUs_Tapped(object sender, EventArgs e)
        {
            var detail = new NavigationPage(new AboutUsPage());
            App.DetailPage = new AboutUsPage();
            detail.Title = "AboutUsPage";
            App.Navigation = detail.Navigation;
            Detail = detail;
            IsPresented = false;
        }

        private void GridLogout_Tapped(object sender, EventArgs e)
        {
            var otherPage = new LoginPage();
            var homePage = App.NavigationPage.Navigation.NavigationStack.First();
            App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
             App.NavigationPage.PopToRootAsync(false);
        }

        private void ImageProfile_Tapped(object sender, EventArgs e)
        {
            var detail = new NavigationPage(new UserProfilePage());
            App.DetailPage = new UserProfilePage();
            detail.Title = "UserProfilePage";
            App.Navigation = detail.Navigation;
            Detail = detail;
            IsPresented = false;
        }
    }
}