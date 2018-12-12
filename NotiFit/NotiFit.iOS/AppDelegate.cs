﻿using System;
using System.Collections.Generic;
using System.Linq;
using AsNum.XFControls.iOS;
using Com.OneSignal;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Foundation;

using UIKit;

namespace NotiFit.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {   //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
           
            AsNumAssemblyHelper.HoldAssembly();
            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);
            OneSignal.Current.StartInit("0c5b7cb6-a33d-453e-ada0-2c54d17bd7ba")                
                     .EndInit();
            OneSignal.Current.RegisterForPushNotifications();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }


    }
}
