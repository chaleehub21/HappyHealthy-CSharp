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
        EditText txtName, txtAge, txtIdenNo, txtSex, txtWeight, txtHeight;
        RadioButton mRadio, fRadio;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_display_user);
            txtName = FindViewById<EditText>(Resource.Id.tv_Name);
            txtAge = FindViewById<EditText>(Resource.Id.tv_Age);
            txtIdenNo = FindViewById<EditText>(Resource.Id.ud_iden);
            txtSex = FindViewById<EditText>(Resource.Id.ud_gen);
            var tempLogOut = FindViewById<ImageView>(Resource.Id.logout);
            
            
            tempLogOut.Click += delegate {
                GlobalFunction.clearAllPreference(this);
                StartActivity(new Intent(this, typeof(Login)));
                this.Finish();
            };
            InitializeUserData();
            var updateButton = FindViewById<TextView>(Resource.Id.savedatauser);
            updateButton.Click += UpdateUserInfo;
        }

        private void UpdateUserInfo(object sender, EventArgs e)
        {
            UserTABLE.UpdateUserToSQL(txtName.Text, txtSex.Text[0], txtIdenNo.Text,null, null, this);
        }

        private void InitializeUserData()
        {
            var user = new UserTABLE();
            user = user.getUserDetail(GlobalFunction.getPreference("ud_id", "", this));
            txtAge.Text = (DateTime.Now.Year - user.ud_birthdate.Year).ToString();
            txtName.Text = user.ud_name;
            txtIdenNo.Text = user.ud_iden_number;
            txtSex.Text = Convert.ToString(GlobalFunction.StringValidation(user.ud_gender));

        }
    }
}