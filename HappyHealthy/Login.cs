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

namespace HappyHealthyCSharp
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        private static readonly int ButtonClickNotificationId = 1000;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            SetContentView(Resource.Layout.activity_login);
            // Create your application here
            var id = FindViewById<EditText>(Resource.Id.userID);
            var pw = FindViewById<EditText>(Resource.Id.userPW);
            var login = FindViewById<ImageView>(Resource.Id.loginBtt);
            var register = FindViewById<TextView>(Resource.Id.textViewRegis);
            var forgot = FindViewById<TextView>(Resource.Id.textViewForget);
            id.Text = "kunvutloveza@hotmail.com";
            pw.Text = "123456";
            login.Click += delegate {
                var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
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
                            GlobalFunction.setPreference("ud_id", reader.GetString(1),this);
                            StartActivity(typeof(MainActivity));
                            this.Finish();
                        }
                        else
                        {
                            GlobalFunction.createDialog(this, "Access Denied").Show();
                        }
                    }
                    else
                    {
                        GlobalFunction.createDialog(this, "Access Denied").Show();
                    }
                }
                catch {
                    GlobalFunction.createDialog(this,"Database is not available").Show();
                }
            };
            register.Click += delegate {
                StartActivity(new Intent(this, typeof(Register)));
            };
            forgot.Click += delegate {
                var Thread = new System.Threading.Thread(()=> {
                    var conn = new MySqlConnection(GlobalFunction.remoteaccess);
                    object result = null;
                    try
                    {
                        conn.Open();
                        var sql = $@"SELECT ud_pass FROM user_detail WHERE ud_email = '{(id.Text)}'";
                        var cmd = new MySqlCommand(sql, conn);
                        cmd.CommandText = sql;
                        result = cmd.ExecuteScalar();
                        GlobalFunction.SendMail("securapp.assist@gmail.com", "securapp7421", id.Text, "Hello", $@"<body><h2>Password for <b>{id.Text}</b></h2><br><h2>{(string)result}</h2></body>");
                    }
                    catch
                    {
                        return;
                    }
                    
                });
                Thread.Start();
                
                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                notificationManager.Notify(ButtonClickNotificationId, Notification.setNotification(this, $"Password recovery email has been sent to {id.Text}", typeof(Login)).Build());
               

            };
        }
      
    }
}