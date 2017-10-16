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
using Android.Media;
using Android.Icu.Util;

namespace HappyHealthyCSharp
{
    class CustomNotification : Activity
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
        public static void Show(Context c,string content,DateTime alertTime,int customSoundResourceID = -9999)
        {
            /*
            var filePath = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            if(customSoundResourceID != -9999)
            {
                filePath = Android.Net.Uri.Parse($@"android.resource://{c.PackageName}/{customSoundResourceID}");
            }
            var notificationManager = (NotificationManager)c.GetSystemService(Context.NotificationService);
            notificationManager.Notify(ButtonClickNotificationId, CustomNotification.setNotification(c, content, target)
                //.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm))
                //.SetSound(Android.Net.Uri.Parse(c.Resources.GetResourceName(Resource.Raw.notialert)))
                .SetSound(filePath)
                .Build());
             */
            var filePath = RingtoneManager.GetDefaultUri(RingtoneType.Notification).Path;
            if (customSoundResourceID != -9999)
            {
                filePath = $@"android.resource://{c.PackageName}/{customSoundResourceID}";
            }
            var alarmIntent = new Intent(c, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", $"{content}");
            alarmIntent.PutExtra("title", "HappyHealthy");
            alarmIntent.PutExtra("sound", filePath);
            var pendingIntent = PendingIntent.GetBroadcast(c, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var time = alertTime.AddSeconds(5);
            var manager = (AlarmManager)c.GetSystemService(Context.AlarmService);
            var calendar = Java.Util.Calendar.Instance;
            calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
            calendar.Set(time.Year, time.Month - 1, time.Day, time.Hour, time.Minute, time.Second);
            /*
            var halfMin = (long)(30 * 1000);
            manager.SetRepeating(AlarmType.RtcWakeup, calendar.TimeInMillis, halfMin, pendingIntent);
            */ //This code snippet is worked!!!, so let's assume it's work for IntervalDay as well as halfMin
            manager.SetRepeating(AlarmType.RtcWakeup, calendar.TimeInMillis, AlarmManager.IntervalDay, pendingIntent);
            //reference : https://stackoverflow.com/questions/42237920/xamarin-android-how-to-schedule-and-alarm-with-a-broadcastreceiver
        }
        public static bool CancelAllAlarmManager(Context c,Intent intent,int requestCode = 0)
        {
            var alarmManager = (AlarmManager)c.GetSystemService(Context.AlarmService);
            var updateServiceIntent = intent;
            var pendingUpdateIntent = PendingIntent.GetService(c, requestCode, updateServiceIntent, 0);
            try
            {
                alarmManager.Cancel(pendingUpdateIntent);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    [BroadcastReceiver]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");
            var sPath = intent.GetStringExtra("sound");
            var soundPath = Android.Net.Uri.Parse(sPath);
            var resultIntent = new Intent(context, typeof(EmptyDemo));
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            var pending = PendingIntent.GetActivity(context, 0,resultIntent,PendingIntentFlags.CancelCurrent);
            var builder = new Notification.Builder(context)
                             .SetContentTitle(title)
                             .SetContentText(message)
                             .SetSmallIcon(Resource.Drawable.Icon)
                             .SetSound(soundPath)
                             .SetVibrate(new long[0]);
            builder.SetContentIntent(pending);
            var notification = builder.Build();
            var manager = NotificationManager.FromContext(context);
            manager.Notify(1337, notification);

        }
    }
}