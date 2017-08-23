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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            SetContentView(Resource.Layout.activity_login);
            // Create your application here
            var id = FindViewById<EditText>(Resource.Id.userID);
            var pw = FindViewById<EditText>(Resource.Id.userPW);
            var login = FindViewById<ImageView>(Resource.Id.loginBtt);
            id.Text = "kunvutloveza@hotmail.com";
            pw.Text = "123456";
            login.Click += delegate {
                var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
                var comm = sqlconn.CreateCommand();
                var userRow = new JavaList<IDictionary<string, object>>();
                var sqlQuery = $@"select count(ud_id) from user_detail where ud_email = @id and ud_pass = @pw";
                comm.CommandText = sqlQuery;
                comm.Parameters.AddWithValue("@id", id.Text);
                comm.Parameters.AddWithValue("@pw", pw.Text);
                sqlconn.Open();
                var reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (Convert.ToInt32(reader.GetString(0)) == 1)
                    {
                        StartActivity(typeof(MainActivity));
                    }
                    else
                    {
                        GlobalFunction.createDialog(this, "Access Denied").Show();
                    }
                    GlobalFunction.createDialog(this, comm.CommandText).Show();
                }
                else
                {
                    GlobalFunction.createDialog(this, "Access Denied").Show();
                }    
            };
        }
    }
}