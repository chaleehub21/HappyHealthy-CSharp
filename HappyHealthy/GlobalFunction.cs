﻿using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech.Tts;
namespace HappyHealthyCSharp
{
    public class GlobalFunction
    {
        public static string dbPath = "server=10.0.2.2;port=3306;database=happyhealthydb;User Id=root;Password=lovelove12;charset=utf8";
        //public static string remoteaccess = "server=192.168.137.1;port=3306;database=ckd;User Id=root;Password=thisisapassword;charset=utf8";//192.168.137.1
        public static string remoteaccess = "server=10.0.2.2;port=3306;database=ckd;User Id=root;Password=lovelove12;charset=utf8";
        /// <summary>
        /// Simple dialog box for just showing the message.
        /// </summary>
        /// <param name="t">Base Caller</param>
        /// <param name="message">Dialog Message</param>
        public static AlertDialog.Builder createDialog(Context t, string message, EventHandler<DialogClickEventArgs> action = null, EventHandler<DialogClickEventArgs> cancelAction = null, string actionMessage = "OK", string cancelActionMessage = "Cancel")
        {
            var isCancelAble = cancelAction == null ? false : true; //is cancel action function defined? if yes then the cancel button should be available.
            var nDLG = new AlertDialog.Builder(t);
            nDLG
                .SetMessage(message)
                .SetNeutralButton(actionMessage, action);          
            if (isCancelAble == true)
            {
                nDLG
                .SetCancelable(isCancelAble)
                .SetNegativeButton(cancelActionMessage, cancelAction);
            }
            return nDLG;
        }
        public static void setPreference(string key, string value, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString(key, value);
            editor.Apply();
        }
        public static void setPreference(string key, int value, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutInt(key, value);
            editor.Apply();
        }
        public static void setPreference(string key, bool value, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean(key, value);
            editor.Apply();
        }
        public static void setPreference(string key, float value, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutFloat(key, value);
            editor.Apply();
        }
        public static string getPreference(string key, string defVal, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            return prefs.GetString(key, defVal);
        }
        public static int getPreference(string key, int defVal, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            return prefs.GetInt(key, defVal);
        }
        public static bool getPreference(string key, bool defVal, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            return prefs.GetBoolean(key, defVal);
        }
        public static float getPreference(string key, float defVal, Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            return prefs.GetFloat(key, defVal);
        }
        public static void clearAllPreference(Context c)
        {
            ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(c);
            prefs.Edit().Clear().Commit();
        }
        public static string stringValidation<T>(T t)
        {
            if(Double.TryParse(t.ToString(),out double val) == true)
            {
                return (Math.Round(Convert.ToDouble(t), 2)).ToString();
            }
            if (ReferenceEquals(t,null))
            {
                return "";
            }
            return t.ToString();
        }
    }
}