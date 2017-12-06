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
using Android.Speech;

namespace HappyHealthyCSharp
{
    [Activity(Theme = "@style/MyMaterialTheme.Base")]
    class Home : Activity
    {
        TextView labelTest;
        #region Experimental Section
        private bool isRecording;
        private readonly int VOICE = 10;
        private Button recButton;
        private EditText testResult;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_home);
            ImageView DiabetesButton = FindViewById<ImageView>(Resource.Id.imageView_button_diabetes);
            ImageView KidneyButton = FindViewById<ImageView>(Resource.Id.imageView_button_ckd);
            ImageView PressureButton = FindViewById<ImageView>(Resource.Id.imageView_button_pressure);
            ImageView FoodButton = FindViewById<ImageView>(Resource.Id.imageView_button_food);
            ImageView MedicineButton = FindViewById<ImageView>(Resource.Id.imageView_button_pill);
            ImageView DoctorButton = FindViewById<ImageView>(Resource.Id.imageView_button_doctor);
            DiabetesButton.Click += ClickDiabetes;
            KidneyButton.Click += ClickKidney;
            PressureButton.Click += ClickPressure;
            FoodButton.Click += ClickFood;
            MedicineButton.Click += ClickPill;
            DoctorButton.Click += ClickDoctor;
            //TestSTTImplementation();
            var dev = FindViewById<ImageView>(Resource.Id.imageView4);
            dev.Click += delegate {
                Extension.CreateDialogue(this, "Not Implemented").Show();
            };
        }

        private void TestSTTImplementation()
        {
            var imageView = FindViewById<ImageView>(Resource.Id.imageView4);
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {
                // no microphone, no recording. Disable the button and output an alert
                Extension.CreateDialogue(this, "ไม่พบไมโครโฟนบนระบบของคุณ").Show();
            }
            else
                imageView.Click += delegate
                {
                    isRecording = !isRecording;
                    if (isRecording)
                    {
                        var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak Now!");
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                        StartActivityForResult(voiceIntent, VOICE);
                    }
                    //GlobalFunction.CreateDialogue(this, GlobalFunction.getPreference("ud_id", "not found", this)).Show();
                };
            //Toast.MakeText(this, CustomNotification.CancelAllAlarmManager(this, new Intent(this, typeof(AlarmReceiver))) ? "TRUE" : "FALSE", ToastLength.Long).Show();
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = matches[0];

                        // limit the output to 500 characters
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);
                        //GlobalFunction.createDialog(this, textInput).Show();
                        Toast.MakeText(this, textInput, ToastLength.Short);
                        labelTest = FindViewById<TextView>(Resource.Id.textView18);
                        labelTest.Text = textInput;
                    }
                    else
                        Toast.MakeText(this, "Unrecognized", ToastLength.Short);
                }
            }
            base.OnActivityResult(requestCode, resultVal, data);
        }

        public void ClickFood(object sender, EventArgs e)
        {
            Extension.CreateDialogue(this, "Not Implemented").Show();
            /*
            if (MySQLDatabaseHelper.TestConnection(Extension.remoteAccess) == true)
            {
                StartActivity(new Intent(this, typeof(History_Food)));
            }
            else
            {
                Toast.MakeText(this, "Server is unavailable right at the moment, please try again later.", ToastLength.Short).Show();
            }
            */
        }
        private void ManualDataSync()
        {
            var Service = new HHCSService1.HHCSService();
            try
            {
                var diaList = new List<HHCSService1.TEMP_DiabetesTABLE>();
                var kidList = new List<HHCSService1.TEMP_KidneyTABLE>();
                var presList = new List<HHCSService1.TEMP_PressureTABLE>();
                new TEMP_DiabetesTABLE().Select<TEMP_DiabetesTABLE>($"SELECT * FROM TEMP_DiabetesTABLE WHERE ud_id = '{Extension.getPreference("ud_id", 0, this)}'").ForEach(row =>
                {
                    var wsObject = new HHCSService1.TEMP_DiabetesTABLE();
                    wsObject.fbs_id_pointer = row.fbs_id_pointer;
                    wsObject.fbs_time_new = row.fbs_time_new;
                    wsObject.fbs_time_old = row.fbs_time_old;
                    wsObject.fbs_time_string_new = row.fbs_time_string_new;
                    wsObject.fbs_fbs_new = row.fbs_fbs_new;
                    wsObject.fbs_fbs_old = row.fbs_fbs_old;
                    wsObject.fbs_fbs_lvl_new = row.fbs_fbs_lvl_new;
                    wsObject.fbs_fbs_lvl_old = row.fbs_fbs_lvl_old;
                    wsObject.mode = row.mode;
                    diaList.Add(wsObject);
                });
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
                new TEMP_PressureTABLE().Select<TEMP_PressureTABLE>($"SELECT * FROM TEMP_PressureTABLE WHERE ud_id = '{Extension.getPreference("ud_id", 0, this)}'").ForEach(row =>
                {
                    var wsObject = new HHCSService1.TEMP_PressureTABLE();
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
                var result = Service.SynchonizeData(Extension.getPreference("ud_email", string.Empty, this), Extension.getPreference("ud_pass", string.Empty, this), diaList.ToArray(), kidList.ToArray(), presList.ToArray());
                diaList.Clear();
                kidList.Clear();
                presList.Clear();
                result.ToList().ForEach(r =>
                {
                    Console.WriteLine("WEB SERVICE RESPONSE : " + r);
                });
                if (result.ToList().Count > 0)
                {
                    Toast.MakeText(this, "Success", ToastLength.Short).Show();
                    var sqliteInstance = new SQLite.SQLiteConnection(Extension.sqliteDBPath);
                    sqliteInstance.Execute($"DELETE FROM TEMP_DiabetesTABLE WHERE ud_id = {Extension.getPreference("ud_id", 0, this)}");
                    sqliteInstance.Execute($"DELETE FROM TEMP_KidneyTABLE WHERE ud_id = {Extension.getPreference("ud_id", 0, this)}");
                    sqliteInstance.Execute($"DELETE FROM TEMP_PressureTABLE WHERE ud_id = {Extension.getPreference("ud_id", 0, this)}");
                    //sqliteInstance.Query<TEMP_DiabetesTABLE>($"SELECT * FROM TEMP_DiabetesTABLE WHERER ud_id = '{Extension.getPreference("ud_id", 0, this)}'");
                    //sqliteInstance.Query<TEMP_KidneyTABLE>($"SELECT * FROM FROM TEMP_KidneyTABLE WHERER ud_id = '{Extension.getPreference("ud_id", 0, this)}'");
                    //sqliteInstance.Query<TEMP_PressureTABLE>($"SELECT * FROM TEMP_PressureTABLE WHERER ud_id = '{Extension.getPreference("ud_id", 0, this)}'");
                    MySQLDatabaseHelper.GetDataFromMySQLToSQLite(Extension.getPreference("ud_email", string.Empty, this), Extension.getPreference("ud_pass", string.Empty, this));
                }
                else
                {
                    Toast.MakeText(this, "Failure", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : " + ex.ToString());
            }
        }
        public void ClickDiabetes(object sender,EventArgs e)
        {
            StartActivity(new Intent(this, typeof(History_Diabetes)));
        }
        public void ClickKidney(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(History_Kidney)));
        }
        public void ClickPressure(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(History_Pressure)));
        }
        public void ClickDevelop(object sender, EventArgs e)
        {
            //StartActivity(new Intent(this, typeof(Develop)));
            //GlobalFunction.createDialog(this, "Not implemented").Show();
           
        }
        public void ClickPill(object sender, EventArgs e)
        {
            //GlobalFunction.createDialog(this, "Not implemented").Show();
            StartActivity(new Intent(this, typeof(History_Pill)));
        }
        public void ClickDoctor(object sender, EventArgs e)
        {
            StartActivity(typeof(History_Doctor));
        }
    }
}