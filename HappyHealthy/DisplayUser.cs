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
    
    [Activity(Theme = "@style/MyMaterialTheme.Base")]
    class DisplayUser : Activity
    {
        SQLiteConnection conn = null;
        private EditText TVName, TVAge, TVWeight, TVHeigh;
        private TextView TVBMR, TVBMI, weightStdtextview;
        private string strBMR, strBMI, weightStdstring, strChoseGender;
        private RadioButton male, female;
        private RadioGroup User_gender;
        double dBMR, dBMI;
        int userID;
        Dictionary<string, string> dataUser, dataHistory;
        SimpleDateFormat df_show;
        CalendarView c;
       
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_display_user);
            //instatantiate widget control
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            TVName = FindViewById<EditText>(Resource.Id.tv_Name);
            TVAge = FindViewById<EditText>(Resource.Id.tv_Age);
            TVWeight = FindViewById<EditText>(Resource.Id.tv_Weight);
            TVHeigh = FindViewById<EditText>(Resource.Id.tv_Height);
            TVBMR = FindViewById<TextView>(Resource.Id.tv_BMR);
            TVBMI = FindViewById<TextView>(Resource.Id.tv_BMI);
            weightStdtextview = FindViewById<TextView>(Resource.Id.weightStdTextView);
            male = FindViewById<RadioButton>(Resource.Id.male);
            female = FindViewById<RadioButton>(Resource.Id.female);
            User_gender = FindViewById<RadioGroup>(Resource.Id.User_Gender);
            //do the jobs
            
            

        }
        private void defaultValue()
        {
            //then clear its value
            TVName.Text = "";
            TVHeigh.Text = "";
            TVWeight.Text = "";
            TVBMI.Text = "";
            TVBMR.Text = "";
            TVAge.Text = "";
            weightStdtextview.Text = "";
            User_gender.ClearCheck();
        }
    }
}