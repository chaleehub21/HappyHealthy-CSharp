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
using MySql.Data.MySqlClient;
using System.Data;
using Plugin.LocalNotifications;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            SetContentView(Resource.Layout.activity_login);
            // Create your application here

            //is Cache data available?
            if ((Extension.getPreference("ud_id", 0, this) != 0))
            {
                StartActivity(typeof(MainActivity));
                Finish();
            }
            //
            var id = FindViewById<EditText>(Resource.Id.userID);
            var pw = FindViewById<EditText>(Resource.Id.userPW);
            var login = FindViewById<ImageView>(Resource.Id.loginBtt);
            var register = FindViewById<TextView>(Resource.Id.textViewRegis);
            var forgot = FindViewById<TextView>(Resource.Id.textViewForget);
            id.Text = "kunvutloveza@hotmail.com";
            pw.Text = "123456";
            login.Click += delegate
            {
                if(new UserTABLE().Select<UserTABLE>($"SELECT * FROM UserTABLE").Count == 0)
                {
                    MySQLDatabaseHelper.GetDataFromMySQLToSQLite(id.Text, pw.Text);
                }
                try
                {
                    if (AccountHelper.ComparePassword(pw.Text, new UserTABLE().Select<UserTABLE>($"SELECT * FROM UserTABLE WHERE ud_email = '{id.Text}'")[0].ud_pass))
                    {
                        Initialization(id);
                        StartActivity(typeof(MainActivity));
                        this.Finish();
                    }
                    else
                    {
                        Extension.CreateDialogue(this, "Access Denied").Show();
                    }
                }
                catch
                {
                    Extension.CreateDialogue(this, "Access Denied").Show();
                }
            };
            register.Click += delegate
            {
                StartActivity(new Intent(this, typeof(Register)));
            };
            forgot.Click += delegate
            {
                StartActivity(new Intent(this, typeof(PasswordResetActivity)));
                //CustomNotification.SetAlarmManager(this, "Sender!",(int)DateTime.Now.DayOfWeek,DateTime.Now,Resource.Raw.notialert);
                //CrossLocalNotifications.Current.Show("HH", "TRUE!!!", 101, DateTime.Now.AddSeconds(10));
            };
        }

        private void Initialization(EditText id)
        {
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
            var sql = $@"select * from UserTABLE where ud_email = '{id.Text}'";
            var result = conn.Query<UserTABLE>(sql);
            Extension.setPreference("ud_email", result[0].ud_email, this);
            Extension.setPreference("ud_pass", result[0].ud_pass, this);
            Extension.setPreference("ud_id", result[0].ud_id, this);
        }
    }
}