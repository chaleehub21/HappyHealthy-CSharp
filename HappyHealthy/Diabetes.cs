﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using MySql.Data.MySqlClient;
namespace HappyHealthyCSharp
{
    [Activity]
    public class Diabetes : Activity
    {
        ListView listview;
        EditText BloodValue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_diabetes);
            // Create your application here
            BloodValue = FindViewById<EditText>(Resource.Id.sugar_value);
        }
        [Export("ClickDisLevelsSugar")]
        public void ClickDisLevelsSugar(View v)
        {
            var diaTable = new DiabetesTABLE();
            diaTable.InsertFbsToSQL(BloodValue.Text, 1);
            GlobalFunction.createDialog(this, $@"Inserted",delegate { this.Finish(); }).Show();

        }
        [Export("ClickBackDiaHome")]
        public void ClickBackDiaHome(View v)
        {
            //StartActivity(new Intent(this, typeof(History_Diabetes)));
            this.Finish();
        }
    }
}