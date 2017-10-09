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
    public class Pressure : Activity
    {
        private EditText BPLow;
        private EditText BPUp;
        private EditText HeartRate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pressure);
            // Create your application here
            BPLow = FindViewById<EditText>(Resource.Id.P_costPressureDown);
            BPUp = FindViewById<EditText>(Resource.Id.P_costPressureTop);
            HeartRate = FindViewById<EditText>(Resource.Id.P_HeartRate);
            var save = FindViewById<ImageView>(Resource.Id.imageView29);
            save.Click += ClickDisLevelsPre;
        }
        public void ClickDisLevelsPre(object sender, EventArgs e)
        {
            var bpTable = new PressureTABLE();
            //bpTable.InsertPressureToSQL(BPUp.Text, BPLow.Text, HeartRate.Text, Convert.ToInt32(GlobalFunction.getPreference("ud_id", "", this)));
            bpTable.bp_up = Convert.ToDecimal(BPUp.Text);
            bpTable.bp_lo = Convert.ToDecimal(BPLow.Text);
            bpTable.bp_hr = Convert.ToInt32(HeartRate.Text);
            bpTable.bp_time = DateTime.Now.ToThaiLocale();
            bpTable.ud_id = Extension.getPreference("ud_id", 0, this);
            bpTable.Insert<PressureTABLE>(bpTable);
            //GlobalFunction.CreateDialogue(this, $@"Inserted", delegate { this.Finish(); }).Show();
            this.Finish();

        }
        [Export("ClickBackPreHome")]
        public void ClickBackPreHome(View v)
        {
            this.Finish();
        }
    }
}