using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using AsNum.XFControls.Droid;
using FFImageLoading.Svg.Forms;
using FFImageLoading.Forms.Platform;
using System.Threading.Tasks;
using Com.OneSignal;
using Plugin.Media;
using Plugin.CurrentActivity;
#region FCM_Lib

#endregion
//using Firebase.Iid;

namespace NotiFit.Droid
{
    // [Activity(Label = "NotiFit", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "NotiFit", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    { 
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);


            base.OnCreate(bundle);           
            AsNumAssemblyHelper.HoldAssembly();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);
            var ignore = typeof(SvgCachedImage);           
            
            OneSignal.Current.StartInit("0c5b7cb6-a33d-453e-ada0-2c54d17bd7ba")
                  .EndInit();
            OneSignal.Current.RegisterForPushNotifications();
            await CrossMedia.Current.Initialize();
            CrossCurrentActivity.Current.Init(this, bundle);
            LoadApplication(new App());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

