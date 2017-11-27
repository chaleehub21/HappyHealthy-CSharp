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
using System.Threading.Tasks;
using System.Threading;

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
            if (pressureObject.bp_hr == Extension.flagValue)
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
                TrySyncWithMySQL();
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
            TrySyncWithMySQL();
            Finish();

        }

        public void SaveValue(object sender, EventArgs e)
        {
            var bpTable = new PressureTABLE();
            try
            {
                bpTable.bp_id = new SQLite.SQLiteConnection(Extension.sqliteDBPath).ExecuteScalar<int>($"SELECT MAX(bp_id)+1 FROM PressureTABLE");
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

            bpTable.bp_time = DateTime.Now.ToThaiLocale();
            bpTable.ud_id = Extension.getPreference("ud_id", 0, this);
            bpTable.Insert();
            TrySyncWithMySQL();
            this.Finish();

        }
        [Export("ClickBackPreHome")]
        public void ClickBackPreHome(View v)
        {
            this.Finish();
        }
        public void TrySyncWithMySQL()
        {
            var t = new Thread(() => {
                var Service = new HHCSService.HHCSService();
                var presList = new List<HHCSService.TEMP_PressureTABLE>();
                new TEMP_PressureTABLE().Select<TEMP_PressureTABLE>($"SELECT * FROM TEMP_PressureTABLE WHERE ud_id = '{Extension.getPreference("ud_id", 0, this)}'").ForEach(row => {
                    var wsObject = new HHCSService.TEMP_PressureTABLE();
                    wsObject.bp_id_pointer = row.bp_id_pointer;
                    wsObject.bp_time_new = row.bp_time_new;
                    wsObject.bp_time_old = row.bp_time_old;
                    wsObject.bp_time_string_new = row.bp_time_string_new;
                    wsObject.bp_up_new = row.bp_up_new;
                    wsObject.bp_up_old = row.bp_up_old;
                    wsObject.bp_lo_new = row.bp_lo_new;
                    wsObject.bp_lo_old = row.bp_lo_old;
                    wsObject.bp_hr_new = row.bp_hr_new;
                    wsObject.bp_hr_old = row.bp_hr_old;
                    wsObject.bp_up_lvl_new = row.bp_up_lvl_new;
                    wsObject.bp_up_lvl_old = row.bp_up_lvl_old;
                    wsObject.bp_lo_lvl_new = row.bp_lo_lvl_new;
                    wsObject.bp_lo_lvl_old = row.bp_lo_lvl_old;
                    wsObject.bp_hr_lvl_new = row.bp_hr_lvl_new;
                    wsObject.bp_hr_lvl_old = row.bp_hr_lvl_old;
                    wsObject.mode = row.mode;
                    presList.Add(wsObject);
                });
                Service.SynchonizeData(Extension.getPreference("ud_email", string.Empty, this)
                    , Extension.getPreference("ud_pass", string.Empty, this)
                    , new List<HHCSService.TEMP_DiabetesTABLE>().ToArray()
                    , new List<HHCSService.TEMP_KidneyTABLE>().ToArray()
                    , presList.ToArray());
                presList.Clear();
            });
            t.Start();
        }
    }
}