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
using SQLite;
using Java.Text;
using MySql.Data.MySqlClient;

namespace HappyHealthyCSharp
{
    
    [Activity(Theme = "@style/MyMaterialTheme.Base")]
    class DisplayUser : Activity
    {
        EditText txtName, txtAge;
        RadioButton mRadio, fRadio;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_display_user);
            txtName = FindViewById<EditText>(Resource.Id.tv_Name);
            txtAge = FindViewById<EditText>(Resource.Id.tv_Age);
            var tempLogOut = FindViewById<ImageView>(Resource.Id.logout);
            var foodExchange = FindViewById<ImageView>(Resource.Id.foodExchangeBtt);
            tempLogOut.Click += delegate {
                GlobalFunction.clearAllPreference(this);
                StartActivity(new Intent(this, typeof(Login)));
                this.Finish();
            };
            InitializeUserData();
        }

        private void InitializeUserData()
        {
            var user = new UserTABLE();
            user = user.getUserDetail(GlobalFunction.getPreference("ud_id", "", this));
            txtAge.Text = (DateTime.Now.Year - user.ud_birthdate.Year).ToString();
            txtName.Text = user.ud_name;

        }
    }
}