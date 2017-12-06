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
    [Activity(Label = "Login",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
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
                AccountLogin(id.Text, pw.Text);
            };
            register.Click += delegate
            {
                StartActivity(new Intent(this, typeof(Register)));
            };
            forgot.Click += delegate
            {
                StartActivity(new Intent(this, typeof(PasswordResetActivity)));
            };
        }

        private void AccountLogin(string id, string pw)
        {
            if (new UserTABLE().Select<UserTABLE>($"SELECT * FROM UserTABLE WHERE ud_email = '{id}'").Count == 0)
            {
                Toast.MakeText(this, "Getting your data from server...", ToastLength.Long);
                MySQLDatabaseHelper.GetDataFromMySQLToSQLite(id, pw);
            }
            if (AccountHelper.ComparePassword(pw, new UserTABLE().Select<UserTABLE>($"SELECT * FROM UserTABLE WHERE ud_email = '{id}'")[0].ud_pass))
            {
                Initialization(id, pw);
                StartActivity(typeof(MainActivity));
                this.Finish();
            }
            else
            {
                Extension.CreateDialogue(this, "Access Denied").Show();
            }
        }

        private void Initialization(string id,string password)
        {
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
            var sql = $@"select * from UserTABLE where ud_email = '{id}'";
            var result = conn.Query<UserTABLE>(sql);
            Extension.setPreference("ud_email", id, this);
            Extension.setPreference("ud_pass", password, this);
            Extension.setPreference("ud_id", result[0].ud_id, this);
        }
    }
}