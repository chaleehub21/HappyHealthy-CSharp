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

namespace HappyHealthyCSharp
{
    [Activity(Label = "Forgot")]
    public class Forgot : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forget_password);
            // Create your application here
            var email = FindViewById<EditText>(Resource.Id.forget_email);
            var backbtt = FindViewById<ImageView>(Resource.Id.forget_back_btt);
            var mailSend = FindViewById<ImageView>(Resource.Id.forget_send_button);
            backbtt.Click += delegate
            {
                this.Finish();
            };
            mailSend.Click += delegate {
                var Thread = new System.Threading.Thread(() => {
                    var conn = new MySqlConnection(GlobalFunction.remoteaccess);
                    object result = null;
                    try
                    {
                        conn.Open();
                        var sql = $@"SELECT ud_pass FROM user_detail WHERE ud_email = '{(email.Text)}'";
                        var cmd = new MySqlCommand(sql, conn);
                        cmd.CommandText = sql;
                        result = cmd.ExecuteScalar();
                        GlobalFunction.SendMail("securapp.assist@gmail.com", "securapp7421", email.Text, "Hello", $@"<body><h2>Password for <b>{email.Text}</b></h2><br><h2>{(string)result}</h2></body>");
                    }
                    catch
                    {
                        return;
                    }

                });
                Thread.Start();
                Toast.MakeText(this, $@"Email has been send to {email.Text}",ToastLength.Short).Show();
            };
        }
    }
}