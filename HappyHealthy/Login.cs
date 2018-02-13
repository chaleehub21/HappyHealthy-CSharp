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
using SQLite;
using System.Threading;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Login", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
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
            Extension.clearAllPreference(this);
            ProgressDialog progressDialog = new ProgressDialog(this);
            login.Click += delegate
            {
                /*
                if (new UserTABLE().Select<UserTABLE>($"SELECT * FROM UserTABLE WHERE ud_email = '{id.Text}'").Count == 0)
                {
                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                    progressDialog.SetTitle("ดาวน์โหลดข้อมูล");
                    progressDialog.SetMessage("กำลังดาวน์โหลดข้อมูลของท่าน กรุณารอสักครู่");
                    progressDialog.Indeterminate = true;
                    progressDialog.SetCancelable(false);
                    progressDialog.Show();
                    await MySQLDatabaseHelper.GetDataFromMySQLToSQLite(id.Text, pw.Text);
                    LogIn(id.Text, pw.Text);
                    progressDialog.Dismiss();
                }
                else
                {
                    LogIn(id.Text, pw.Text);
                }
                */
                var user = new UserTABLE() { ud_id = 3,ud_email = id.Text,ud_pass = AccountHelper.CreatePasswordHash(pw.Text),ud_birthdate = DateTime.Now};
                Initialization(user.ud_email,user.ud_pass);
                StartActivity(typeof(MainActivity));
                this.Finish();
            };
            register.Click += delegate
            {
                StartActivity(new Intent(this, typeof(Register)));
            };
            forgot.Visibility = ViewStates.Invisible;
            forgot.Click += delegate
            {
                StartActivity(new Intent(this, typeof(PasswordResetActivity)));
            };
        }

        private void LogIn(string id, string pw)
        {
            try
            {
                if (AccountHelper.ComparePassword(pw, new UserTABLE().Select<UserTABLE>($"SELECT * FROM UserTABLE WHERE ud_email = '{id}'")[0].ud_pass))
                {
                    Initialization(id, pw);
                    StartActivity(typeof(MainActivity));
                    this.Finish();
                }
                else
                {
                    Extension.CreateDialogue(this, "ข้อมูลเข้าสู่ระบบของท่านผิดพลาด กรุณาตรวจสอบอีกครั้ง").Show();
                }
            }
            catch
            {
                Extension.CreateDialogue(this, "ข้อมูลเข้าสู่ระบบของท่านผิดพลาด กรุณาตรวจสอบอีกครั้ง").Show();
            }
        }

        public void Initialization(string id, string password)
        {
            var conn = new SQLiteConnection(Extension.sqliteDBPath);
            var sql = $@"select * from UserTABLE where ud_email = '{id}'";
            var result = conn.Query<UserTABLE>(sql);
            Extension.setPreference("ud_email", id, this);
            Extension.setPreference("ud_pass", password, this);
            Extension.setPreference("ud_id", result[0].ud_id, this);
        }
    }
}