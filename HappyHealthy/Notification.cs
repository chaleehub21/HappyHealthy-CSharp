using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NotificationCompat = Android.Support.V4.App.NotificationCompat;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using Java.Lang;

namespace HappyHealthyCSharp
{
    class Notification : Activity
    {
        private static readonly int ButtonClickNotificationId = 1000;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        private static NotificationCompat.Builder setNotification(Context c,string content,Type secondActivity)
        {
            var stackBuilder = TaskStackBuilder.Create(c);
            var valuesForActivity = new Bundle();
            var secondIntent = new Intent(c, secondActivity);
            stackBuilder.AddParentStack(Class.FromType(secondActivity));
            stackBuilder.AddNextIntent(secondIntent);
            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
            var builder = new NotificationCompat.Builder(c)
                .SetPriority((int)NotificationPriority.Max)
                .SetAutoCancel(true)
                .SetContentIntent(resultPendingIntent)
                .SetContentTitle("Happy Healthy")
                .SetSmallIcon(Resource.Drawable.iconfood)
                .SetVibrate(new long[0])
                .SetContentText(content);                
            return builder;
            
        }
        public static void Show(Context c,string content,Type target)
        {
            var notificationManager = (NotificationManager)c.GetSystemService(Context.NotificationService);
            notificationManager.Notify(ButtonClickNotificationId, Notification.setNotification(c, content, target).Build());
        }
    }
}