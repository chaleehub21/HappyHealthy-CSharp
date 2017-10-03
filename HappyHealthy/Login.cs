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

            if ((GlobalFunction.getPreference("ud_id",0,this) != 0))
            {
                StartActivity(typeof(MainActivity));
            }
            //
            var id = FindViewById<EditText>(Resource.Id.userID);
            var pw = FindViewById<EditText>(Resource.Id.userPW);
            var login = FindViewById<ImageView>(Resource.Id.loginBtt);
            var register = FindViewById<TextView>(Resource.Id.textViewRegis);
            var forgot = FindViewById<TextView>(Resource.Id.textViewForget);
            id.Text = "kunvutloveza@hotmail.com";
            pw.Text = "123456";
            login.Click += delegate {
                /*
                var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
                var comm = sqlconn.CreateCommand();
                var userRow = new JavaList<IDictionary<string, object>>();
                var sqlQuery = $@"select count(ud_id),ud_id from user_detail where ud_email = @id and ud_pass = @pw";
                comm.CommandText = sqlQuery;
                comm.Parameters.AddWithValue("@id", id.Text);
                comm.Parameters.AddWithValue("@pw", pw.Text);
                try
                {
                    sqlconn.Open();
                    var reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (Convert.ToInt32(reader.GetString(0)) == 1)
                        {
                            GlobalFunction.setPreference("ud_email",id.Text, this);
                            GlobalFunction.setPreference("ud_pass", pw.Text, this);
                            GlobalFunction.setPreference("ud_id", reader.GetString(1),this);
                            StartActivity(typeof(MainActivity));
                            this.Finish();
                        }
                        else
                        {
                            GlobalFunction.CreateDialogue(this, "Access Denied").Show();
                        }
                    }
                    else
                    {
                        GlobalFunction.CreateDialogue(this, "Access Denied").Show();
                    }
                }
                catch {
                    GlobalFunction.CreateDialogue(this,"Database is not available").Show();
                }
                */
                var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
                var sql = $@"select * from UserTABLE where ud_email = '{id.Text}' and ud_pass = '{pw.Text}'";
                var result = conn.Query<UserTABLE>(sql);
                if (result.Count == 1)
                {
                    GlobalFunction.setPreference("ud_email", result[0].ud_email, this);
                    GlobalFunction.setPreference("ud_pass", result[0].ud_pass, this);
                    GlobalFunction.setPreference("ud_id", result[0].ud_id, this);
                    StartActivity(typeof(MainActivity));
                    this.Finish();
                }
                else
                {
                    GlobalFunction.CreateDialogue(this, "Access Denied").Show();
                }

            };
            register.Click += delegate {
                StartActivity(new Intent(this, typeof(Register)));
            };
            forgot.Click += delegate {
                StartActivity(new Intent(this, typeof(Forgot)));
                CustomNotification.Show(this, "Sender!", DateTime.Now,Resource.Raw.notialert);
                //CrossLocalNotifications.Current.Show("HH", "TRUE!!!", 101, DateTime.Now.AddSeconds(10));
            };
        }
      
    }
}