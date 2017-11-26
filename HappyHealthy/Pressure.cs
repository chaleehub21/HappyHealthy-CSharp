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
using Newtonsoft.Json;

namespace HappyHealthyCSharp
{
    [Activity]
    public class Pressure : Activity
    {
        private EditText BPLow;
        private EditText BPUp;
        private EditText HeartRate;
        PressureTABLE pressureObject;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pressure);
            // Create your application here
            BPLow = FindViewById<EditText>(Resource.Id.P_costPressureDown);
            BPUp = FindViewById<EditText>(Resource.Id.P_costPressureTop);
            HeartRate = FindViewById<EditText>(Resource.Id.P_HeartRate);
            var saveButton = FindViewById<ImageView>(Resource.Id.imageView_button_save_pressure);
            var deleteButton = FindViewById<ImageView>(Resource.Id.imageView_button_delete_pressure);
            //code goes below
            var flagObjectJson = Intent.GetStringExtra("targetObject") ?? string.Empty;
            pressureObject = string.IsNullOrEmpty(flagObjectJson) ? new PressureTABLE() { bp_hr = Extension.flagValue } : JsonConvert.DeserializeObject<PressureTABLE>(flagObjectJson);
            if(pressureObject.bp_hr == Extension.flagValue)
            {
                deleteButton.Visibility = ViewStates.Invisible;
                saveButton.Click += SaveValue;
            }
            else
            {
                InitialValueForUpdateEvent();
                saveButton.Click += UpdateValue;
                deleteButton.Click += DeleteValue;
            }
            //end
            
        }

        private void DeleteValue(object sender, EventArgs e)
        {
            Extension.CreateDialogue(this, "Do you want to delete this value?", delegate
            {
                pressureObject.Delete<PressureTABLE>(pressureObject.bp_id);
                Finish();
            }, delegate { }, "Yes", "No").Show();
        }

        private void InitialValueForUpdateEvent()
        {
            BPLow.Text = pressureObject.bp_lo.ToString();
            BPUp.Text = pressureObject.bp_up.ToString();
            HeartRate.Text = pressureObject.bp_hr.ToString();
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            pressureObject.bp_up = Convert.ToDecimal(BPUp.Text);
            pressureObject.bp_lo = Convert.ToDecimal(BPLow.Text);
            pressureObject.bp_hr = Convert.ToInt32(HeartRate.Text);
            pressureObject.Update();
            Finish();

        }

        public void SaveValue(object sender, EventArgs e)
        {
            var bpTable = new PressureTABLE();
            try
            {
                bpTable.bp_id = new SQLite.SQLiteConnection(Extension.sqliteDBPath).ExecuteScalar<int>($"SELECT MAX(ckd_id)+1 FROM KidneyTABLE");
            }
            catch
            {
                bpTable.bp_id = 1;
            }
            bpTable.bp_up = Convert.ToDecimal(BPUp.Text);
#region bp_up_level case
            if (bpTable.bp_up < 120)
                bpTable.bp_up_lvl = 0;
            else if (bpTable.bp_up <= 129)
                bpTable.bp_up_lvl = 1;
            else if (bpTable.bp_up <= 139)
                bpTable.bp_up_lvl = 2;
            else if (bpTable.bp_up <= 159)
                bpTable.bp_up_lvl = 3;
            else if (bpTable.bp_up <= 179)
                bpTable.bp_up_lvl = 4;
            else if (bpTable.bp_up >= 180)
                bpTable.bp_up_lvl = 5;
#endregion
            bpTable.bp_lo = Convert.ToDecimal(BPLow.Text);
#region bp_lo_level case
            if (bpTable.bp_lo < 80)
                bpTable.bp_lo_lvl = 0;
            else if (bpTable.bp_lo <= 84)
                bpTable.bp_lo_lvl = 1;
            else if (bpTable.bp_lo <= 89)
                bpTable.bp_lo_lvl = 2;
            else if (bpTable.bp_lo <= 99)
                bpTable.bp_lo_lvl = 3;
            else if (bpTable.bp_lo <= 109)
                bpTable.bp_lo_lvl = 4;
            else if (bpTable.bp_lo >= 110)
                bpTable.bp_lo_lvl = 5;
#endregion
            bpTable.bp_hr = Convert.ToInt32(HeartRate.Text);

            bpTable.bp_time = DateTime.Now;
            bpTable.ud_id = Extension.getPreference("ud_id", 0, this);
            bpTable.Insert();
            this.Finish();

        }
        [Export("ClickBackPreHome")]
        public void ClickBackPreHome(View v)
        {
            this.Finish();
        }
    }
}