using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace NotiFit.Droid
{
    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.MESSAGING_EVENT"
    })]
    class MyFireMessagingService : FirebaseMessagingService
    {
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            SendNotificatios(message.GetNotification().Body, message.GetNotification().Title);
        }

        public void SendNotificatios(string body, string Header)
        {
            Notification.Builder builder = new Notification.Builder(this);
            builder.SetSmallIcon(Resource.Drawable.Icon);
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);
            builder.SetContentIntent(pendingIntent);
            builder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon));
            builder.SetContentTitle(Header);
            builder.SetContentText(body);
            builder.SetDefaults(NotificationDefaults.Sound);
            builder.SetAutoCancel(true);
            NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(1, builder.Build());
        }
    }
}