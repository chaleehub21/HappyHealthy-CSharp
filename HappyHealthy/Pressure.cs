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

        public void ClickDisLevelsPre(View v)
        {
            var bpTable = new PressureTABLE();
            bpTable.InsertPressureToSQL(BPUp.Text, BPLow.Text, HeartRate.Text, Convert.ToInt32(GlobalFunction.getPreference("ud_id","",this)));
            GlobalFunction.CreateDialogue(this, $@"Inserted", delegate { this.Finish(); }).Show();

        }
        public void ClickDisLevelsPre(object sender, EventArgs e)
        {
            var bpTable = new PressureTABLE();
            bpTable.InsertPressureToSQL(BPUp.Text, BPLow.Text, HeartRate.Text, Convert.ToInt32(GlobalFunction.getPreference("ud_id", "", this)));
            GlobalFunction.CreateDialogue(this, $@"Inserted", delegate { this.Finish(); }).Show();

        }
        [Export("ClickBackPreHome")]
        public void ClickBackPreHome(View v)
        {
            this.Finish();
        }
    }
}