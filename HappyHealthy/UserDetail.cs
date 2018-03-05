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

namespace HappyHealthyCSharp
{
    
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class UserDetail : Activity
    {
        EditText txtName, txtAge, txtIdenNo, txtSex;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_display_user);
            txtName = FindViewById<EditText>(Resource.Id.user_detail_name);
            txtAge = FindViewById<EditText>(Resource.Id.user_detail_age);
            //txtIdenNo = FindViewById<EditText>(Resource.Id.ud_iden);
            txtSex = FindViewById<EditText>(Resource.Id.ud_gen);
            var tempLogOut = FindViewById<ImageView>(Resource.Id.logout);         
            tempLogOut.Click += delegate {
                Extension.clearAllPreference(this);
                StartActivity(new Intent(this, typeof(Login)));
                this.Finish();
            };
            InitializeUserData();
            var updateButton = FindViewById<TextView>(Resource.Id.savedatauser);
            updateButton.Click += UpdateUserInfo;
        }

        private void UpdateUserInfo(object sender, EventArgs e)
        {
            //UserTABLE.UpdateUserToSQL(txtName.Text, txtSex.Text[0], txtIdenNo.Text,null, null, this);
            var user = new UserTABLE().Select<UserTABLE>($@"SELECT * FROM UserTABLE WHERE UD_ID = '{Extension.getPreference("ud_id", 0, this)}'")[0];
            user.ud_name = txtName.Text;
            //user.ud_iden_number = txtIdenNo.Text;
            user.ud_gender = txtSex.Text;
            user.Update();
            Extension.CreateDialogue(this, "User profile has been updated.").Show();

            
        }

        private void InitializeUserData()
        {
            var user = new UserTABLE().Select<UserTABLE>($@"SELECT * FROM UserTABLE WHERE UD_ID = '{Extension.getPreference("ud_id", 0, this)}'")[0];
            //user = user.getUserDetail(GlobalFunction.getPreference("ud_id", string.Empty, this));
            txtAge.Text = (DateTime.Now.Year - user.ud_birthdate.Year).ToString();
            txtName.Text = user.ud_name;
            //txtIdenNo.Text = user.ud_iden_number;
            txtSex.Text = Convert.ToString(Extension.StringValidation(user.ud_gender));

        }
    }
}