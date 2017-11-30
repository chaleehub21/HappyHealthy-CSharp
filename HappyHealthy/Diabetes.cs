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
using MySql.Data.MySqlClient;
using Android.Speech;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace HappyHealthyCSharp
{
    [Activity]
    public class Diabetes : Activity
    {

        EditText BloodValue;
        ImageView micButton, saveButton, deleteButton;
        private bool isRecording;
        private readonly int VOICE = 10;
        DiabetesTABLE diaObject = null;
        Dictionary<string, string> dataNLPList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_diabetes);
            BloodValue = FindViewById<EditText>(Resource.Id.sugar_value);
            micButton = FindViewById<ImageView>(Resource.Id.ic_microphone_diabetes);
            saveButton = FindViewById<ImageView>(Resource.Id.imageView_button_save_diabetes);
            deleteButton = FindViewById<ImageView>(Resource.Id.imageView_button_delete_diabetes);
            // Create your application here
            var flagObjectJson = Intent.GetStringExtra("targetObject") ?? string.Empty;
            diaObject = string.IsNullOrEmpty(flagObjectJson) ? new DiabetesTABLE() { fbs_fbs = Extension.flagValue } : JsonConvert.DeserializeObject<DiabetesTABLE>(flagObjectJson);
            if (diaObject.fbs_fbs == Extension.flagValue)
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

            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {
                // no microphone, no recording. Disable the button and output an alert
                Extension.CreateDialogue(this, "ไม่พบไมโครโฟนบนระบบของคุณ").Show();
            }
            else
                micButton.Click += delegate
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
                };
        }

        private void DeleteValue(object sender, EventArgs e)
        {
            Extension.CreateDialogue(this, "Do you want to delete this value?", delegate
            {
                diaObject.Delete<DiabetesTABLE>(diaObject.fbs_id);
                TrySyncWithMySQL();
                Finish();
            }, delegate { }, "Yes", "No").Show();
        }

        private void InitialValueForUpdateEvent()
        {
            BloodValue.Text = diaObject.fbs_fbs.ToString();
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            diaObject.fbs_fbs = (decimal)double.Parse(BloodValue.Text);
            diaObject.ud_id = Extension.getPreference("ud_id", 0, this);
            diaObject.fbs_time = DateTime.Now.ToThaiLocale();
            diaObject.Update();
            TrySyncWithMySQL();
            this.Finish();
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
                        var textInputList = textInput.Split().ToList();
                        dataNLPList = new Dictionary<string, string>();
                        for (var i = 0; i < textInputList.Count; i += 2)
                        {
                            try
                            {
                                dataNLPList.Add(textInputList[i].ToUpper(), textInputList[i + 1]);
                            }
                            catch
                            {

                            }
                        }
                        //Extension.MapDictToControls(new[] { "น้ำตาล","SUGAR" },new[] { BloodValue},dataNLPList);
                    }
                    else
                        Toast.MakeText(this, "Unrecognized value", ToastLength.Short);
                }
            }
            base.OnActivityResult(requestCode, resultVal, data);
        }

        

        public void SaveValue(object sender, EventArgs e)
        {
            var diaTable = new DiabetesTABLE();
            try
            {
                diaTable.fbs_id = new SQLite.SQLiteConnection(Extension.sqliteDBPath).ExecuteScalar<int>($"SELECT MAX(fbs_id)+1 FROM DiabetesTABLE");
            }
            catch
            {
                diaTable.fbs_id = 1;
            }
            diaTable.fbs_fbs = (decimal)double.Parse(BloodValue.Text);
            if (diaTable.fbs_fbs < 100)
                diaTable.fbs_fbs_lvl = 0;
            else if (diaTable.fbs_fbs <= 125)
                diaTable.fbs_fbs_lvl = 1;
            else if (diaTable.fbs_fbs >= 126)
                diaTable.fbs_fbs_lvl = 2;
            diaTable.ud_id = Extension.getPreference("ud_id", 0, this);
            diaTable.fbs_time = DateTime.Now.ToThaiLocale();
            diaTable.Insert();
            TrySyncWithMySQL();
            this.Finish();
        }

        [Export("ClickBackDiaHome")]
        public void ClickBackDiaHome(View v)
        {
            this.Finish();
        }
        public void TrySyncWithMySQL()
        {
            var t = new Thread(() => {
                try
                {
                    var Service = new HHCSService1.HHCSService();
                    var diaList = new List<HHCSService1.TEMP_DiabetesTABLE>();
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
                    Service.SynchonizeData(Extension.getPreference("ud_email", string.Empty, this)
                        , Extension.getPreference("ud_pass", string.Empty, this)
                        , diaList.ToArray()
                        , new List<HHCSService1.TEMP_KidneyTABLE>().ToArray()
                        , new List<HHCSService1.TEMP_PressureTABLE>().ToArray());
                    diaList.Clear();
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message); //Exception mostly throw only when the server is down
                    //or device is not able to reach the server
                }
            });
            t.Start();
        }
    }
}