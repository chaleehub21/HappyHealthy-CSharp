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
using Android.Provider;
using Java.Util;

namespace HappyHealthyCSharp
{
    public class CalendarHelper
    {
        public static ContentValues GetEventContentValues(int calendarId,string title,string description,int year,int month,int date,int startHour,int endHour, bool allday = false,string timeZone = "UTC+7",string rrule = "")
        {
            ContentValues eventValues = new ContentValues();
            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 4);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, description);     
            eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay, allday);
            // GitHub issue #9 : Event start and end times need timezone support.
            // https://github.com/xamarin/monodroid-samples/issues/9
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC+7");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC+7");
            if(rrule != string.Empty)
            {
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Rrule, rrule);
            }
            if(!allday)
            {
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, CalendarMillisecConverter(year, month, date, startHour, 0));
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, CalendarMillisecConverter(year, month, date, endHour, 0));
            }
            return eventValues;
        }
        public static Android.Net.Uri GetDeleteEventURI(string uri,string baseEvent = "content://com.android.calendar/events")
        {
            Android.Net.Uri eventUri = Android.Net.Uri.Parse(baseEvent);
            var deleteUri = ContentUris.WithAppendedId(eventUri, Convert.ToInt32(uri.Substring(uri.LastIndexOf(@"/") + 1)));
            return deleteUri;
            
        }
        private static long CalendarMillisecConverter(int yr, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Java.Util.CalendarField.DayOfMonth, day);
            c.Set(Java.Util.CalendarField.HourOfDay, hr);
            c.Set(Java.Util.CalendarField.Minute, min);
            c.Set(Java.Util.CalendarField.Month, month);
            c.Set(Java.Util.CalendarField.Year, yr);

            return c.TimeInMillis;
        }
    }
}