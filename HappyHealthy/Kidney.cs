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
using Java.Interop;

namespace HappyHealthyCSharp
{
    [Activity]
    public class Kidney : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_kidney);
            var field_gfr = FindViewById<EditText>(Resource.Id.ckd_gfr);
            var field_creatinine = FindViewById<EditText>(Resource.Id.ckd_creatinine);
            var field_bun = FindViewById<EditText>(Resource.Id.ckd_bun);
            var field_sodium = FindViewById<EditText>(Resource.Id.ckd_sodium);
            var field_potassium = FindViewById<EditText>(Resource.Id.ckd_potassium);
            var date = FindViewById<TextView>(Resource.Id.K_date);
            date.Text = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            //var field_albumin_blood = FindViewById<EditText>(Resource.Id.ckd_albumin_blood);
            //var field_albumin_urine = FindViewById<EditText>(Resource.Id.ckd_albumin_urine);
            //var field_phosphorus_blood = FindViewById<EditText>(Resource.Id.ckd_phosphorus_blood);
            var saveButton = FindViewById<ImageView>(Resource.Id.imageView25);
            saveButton.Click += delegate
            {
                var KidneyTable = new KidneyTABLE();
                KidneyTable.InsertKidneyToSQL(field_gfr.Text, field_creatinine.Text, field_bun.Text, field_sodium.Text, field_potassium.Text, "0", "0", "0", 1);
                GlobalFunction.createDialog(this, $@"Inserted", delegate { this.Finish(); }).Show();
            };
            // Create your application here
        }
        [Export("ClickBackKidHome")]
        public void ClickBackKidHome(View v)
        {
            this.Finish();
        }
    }
   
}