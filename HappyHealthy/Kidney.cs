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
            var field_albumin_blood = FindViewById<EditText>(Resource.Id.ckd_albumin_blood);
            var field_albumin_urine = FindViewById<EditText>(Resource.Id.ckd_albumin_urine);
            var field_phosphorus_blood = FindViewById<EditText>(Resource.Id.ckd_phosphorus_blood);
            var saveButton = FindViewById<ImageView>(Resource.Id.imageView25);
            saveButton.Click += delegate
            {
                var kidney = new KidneyTABLE();
                //KidneyTable.InsertKidneyToSQL(field_gfr.Text, field_creatinine.Text, field_bun.Text, field_sodium.Text, field_potassium.Text, field_albumin_blood.Text, field_albumin_urine.Text, field_phosphorus_blood.Text, Convert.ToInt32(GlobalFunction.getPreference("ud_id", "", this)));
                kidney.ckd_gfr = Convert.ToDecimal(field_gfr.Text);
                kidney.ckd_creatinine = Convert.ToDecimal(field_creatinine.Text);
                kidney.ckd_bun = Convert.ToDecimal(field_bun.Text);
                kidney.ckd_sodium = Convert.ToDecimal(field_sodium.Text);
                kidney.ckd_potassium = Convert.ToDecimal(field_potassium.Text);
                kidney.ckd_albumin_blood = Convert.ToDecimal(field_albumin_blood.Text);
                kidney.ckd_albumin_urine = Convert.ToDecimal(field_albumin_urine.Text);
                kidney.ckd_phosphorus_blood = Convert.ToDecimal(field_phosphorus_blood.Text);
                kidney.ckd_time = DateTime.Now.ToThaiLocale();
                kidney.ud_id = GlobalFunction.getPreference("ud_id", 0, this);
                kidney.Insert<KidneyTABLE>(kidney);
                //GlobalFunction.CreateDialogue(this, $@"Inserted", delegate { this.Finish(); }).Show();
                this.Finish();
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