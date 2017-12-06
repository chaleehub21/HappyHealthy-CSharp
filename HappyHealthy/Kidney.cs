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
    public class Kidney : Activity
    {
        KidneyTABLE kidneyObject = null;
        EditText field_gfr;
        EditText field_creatinine;
        EditText field_bun;
        EditText field_sodium;
        EditText field_potassium;
        EditText field_albumin_blood;
        EditText field_albumin_urine;
        EditText field_phosphorus_blood;
        ImageView saveButton, deleteButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_kidney);
            field_gfr = FindViewById<EditText>(Resource.Id.ckd_gfr);
            field_creatinine = FindViewById<EditText>(Resource.Id.ckd_creatinine);
            field_bun = FindViewById<EditText>(Resource.Id.ckd_bun);
            field_sodium = FindViewById<EditText>(Resource.Id.ckd_sodium);
            field_potassium = FindViewById<EditText>(Resource.Id.ckd_potassium);
            field_albumin_blood = FindViewById<EditText>(Resource.Id.ckd_albumin_blood);
            field_albumin_urine = FindViewById<EditText>(Resource.Id.ckd_albumin_urine);
            field_phosphorus_blood = FindViewById<EditText>(Resource.Id.ckd_phosphorus_blood);
            saveButton = FindViewById<ImageView>(Resource.Id.imageView_button_save_kidney);
            deleteButton = FindViewById<ImageView>(Resource.Id.imageView_button_delete_kidney);
            //code goes below
            var flagObjectJson = Intent.GetStringExtra("targetObject") ?? string.Empty;
            kidneyObject = string.IsNullOrEmpty(flagObjectJson) ? new KidneyTABLE() { ckd_gfr = Extension.flagValue } : JsonConvert.DeserializeObject<KidneyTABLE>(flagObjectJson);
            if (kidneyObject.ckd_gfr == Extension.flagValue)
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
                kidneyObject.Delete<KidneyTABLE>(kidneyObject.ckd_id);
                TrySyncWithMySQL();
                Finish();
            }, delegate { }, "Yes", "No").Show();
        }

        private void InitialValueForUpdateEvent()
        {
            field_gfr.Text = kidneyObject.ckd_gfr.ToString();
            field_creatinine.Text = kidneyObject.ckd_creatinine.ToString();
            field_bun.Text = kidneyObject.ckd_bun.ToString();
            field_sodium.Text = kidneyObject.ckd_sodium.ToString();
            field_potassium.Text = kidneyObject.ckd_potassium.ToString();
            field_albumin_blood.Text = kidneyObject.ckd_albumin_blood.ToString();
            field_albumin_urine.Text = kidneyObject.ckd_albumin_urine.ToString();
            field_phosphorus_blood.Text = kidneyObject.ckd_phosphorus_blood.ToString();
        }

        private void SaveValue(object sender, EventArgs e)
        {
            var kidney = new KidneyTABLE();
            try
            {
                kidney.ckd_id = new SQLite.SQLiteConnection(Extension.sqliteDBPath).ExecuteScalar<int>($"SELECT MAX(ckd_id)+1 FROM KidneyTABLE");
            }
            catch
            {
                kidney.ckd_id = 1;
            }
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
            kidney.ud_id = Extension.getPreference("ud_id", 0, this);
            kidney.Insert();
            TrySyncWithMySQL();
            this.Finish();
        }

        private void UpdateValue(object sender, EventArgs e)
        {

            kidneyObject.ckd_gfr = Convert.ToDecimal(field_gfr.Text);
            kidneyObject.ckd_creatinine = Convert.ToDecimal(field_creatinine.Text);
            kidneyObject.ckd_bun = Convert.ToDecimal(field_bun.Text);
            kidneyObject.ckd_sodium = Convert.ToDecimal(field_sodium.Text);
            kidneyObject.ckd_potassium = Convert.ToDecimal(field_potassium.Text);
            kidneyObject.ckd_albumin_blood = Convert.ToDecimal(field_albumin_blood.Text);
            kidneyObject.ckd_albumin_urine = Convert.ToDecimal(field_albumin_urine.Text);
            kidneyObject.ckd_phosphorus_blood = Convert.ToDecimal(field_phosphorus_blood.Text);
            kidneyObject.ud_id = Extension.getPreference("ud_id", 0, this);
            kidneyObject.Update();
            TrySyncWithMySQL();
            this.Finish();
        }

        [Export("ClickBackKidHome")]
        public void ClickBackKidHome(View v)
        {
            this.Finish();
        }
        public void TrySyncWithMySQL()
        {
            var t = new Thread(() =>
            {
                try
                {
                    var Service = new HHCSService1.HHCSService();
                    var kidList = new List<HHCSService1.TEMP_KidneyTABLE>();
                    new TEMP_KidneyTABLE().Select<TEMP_KidneyTABLE>($"SELECT * FROM TEMP_KidneyTABLE WHERE ud_id = '{Extension.getPreference("ud_id", 0, this)}'").ForEach(row =>
                    {
                        var wsObject = new HHCSService1.TEMP_KidneyTABLE();
                        wsObject.ckd_id_pointer = row.ckd_id_pointer;
                        wsObject.ckd_time_new = row.ckd_time_new;
                        wsObject.ckd_time_old = row.ckd_time_old;
                        wsObject.ckd_time_string_new = row.ckd_time_string_new;
                        wsObject.ckd_gfr_new = row.ckd_gfr_new;
                        wsObject.ckd_gfr_old = row.ckd_gfr_old;
                        wsObject.ckd_gfr_level_new = row.ckd_gfr_level_new;
                        wsObject.ckd_gfr_level_old = row.ckd_gfr_level_old;
                        wsObject.ckd_creatinine_new = row.ckd_creatinine_new;
                        wsObject.ckd_creatinine_old = row.ckd_creatinine_old;
                        wsObject.ckd_bun_new = row.ckd_bun_new;
                        wsObject.ckd_bun_old = row.ckd_bun_old;
                        wsObject.ckd_sodium_new = row.ckd_sodium_new;
                        wsObject.ckd_sodium_old = row.ckd_sodium_old;
                        wsObject.ckd_potassium_new = row.ckd_potassium_new;
                        wsObject.ckd_potassium_old = row.ckd_potassium_old;
                        wsObject.ckd_albumin_blood_new = row.ckd_albumin_blood_new;
                        wsObject.ckd_albumin_blood_old = row.ckd_albumin_blood_old;
                        wsObject.ckd_albumin_urine_new = row.ckd_albumin_urine_new;
                        wsObject.ckd_albumin_urine_old = row.ckd_albumin_urine_old;
                        wsObject.ckd_phosphorus_blood_new = row.ckd_phosphorus_blood_new;
                        wsObject.ckd_phosphorus_blood_old = row.ckd_phosphorus_blood_old;
                        wsObject.mode = row.mode;
                        kidList.Add(wsObject);
                    });
                    Service.SynchonizeData(Extension.getPreference("ud_email", string.Empty, this)
                        , Extension.getPreference("ud_pass", string.Empty, this)
                        , new List<HHCSService1.TEMP_DiabetesTABLE>().ToArray()
                        , kidList.ToArray()
                        , new List<HHCSService1.TEMP_PressureTABLE>().ToArray());
                    kidList.Clear();
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
            t.Start();
        }
    }

}