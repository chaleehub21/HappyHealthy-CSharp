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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_diabetes);
            BloodValue = FindViewById<EditText>(Resource.Id.sugar_value);
            micButton = FindViewById<ImageView>(Resource.Id.ic_micro);
            saveButton = FindViewById<ImageView>(Resource.Id.layout_imageview_update_diabetes);
            deleteButton = FindViewById<ImageView>(Resource.Id.layout_imageview_delete_diabetes);
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
                    //GlobalFunction.CreateDialogue(this, GlobalFunction.getPreference("ud_id", "not found", this)).Show();
                };
        }

        private void DeleteValue(object sender, EventArgs e)
        {
            Extension.CreateDialogue(this, "Do you want to delete this value?", delegate
            {
                diaObject.Delete<DiabetesTABLE>(diaObject.fbs_id);
                Finish();
            }, delegate { }, "Yes", "No").Show();
        }

        private void InitialValueForUpdateEvent()
        {
            BloodValue.Text = diaObject.fbs_fbs.ToString();
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            //diaTable.InsertFbsToSQL(BloodValue.Text, Convert.ToInt32(GlobalFunction.getPreference("ud_id", "", this)));
            diaObject.fbs_fbs = (decimal)double.Parse(BloodValue.Text);
            diaObject.ud_id = Extension.getPreference("ud_id", 0, this);
            diaObject.fbs_time = DateTime.Now.ToThaiLocale();
            diaObject.Update();
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
                        if (int.TryParse(textInput,out int parseResult))
                            BloodValue.Text = textInput;
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
            //diaTable.InsertFbsToSQL(BloodValue.Text, Convert.ToInt32(GlobalFunction.getPreference("ud_id", "", this)));
            diaTable.fbs_fbs = (decimal)double.Parse(BloodValue.Text);
            diaTable.ud_id = Extension.getPreference("ud_id", 0, this);
            diaTable.fbs_time = DateTime.Now.ToThaiLocale();
            diaTable.Insert(diaTable);
            this.Finish();

        }
        [Export("ClickBackDiaHome")]
        public void ClickBackDiaHome(View v)
        {
            //StartActivity(new Intent(this, typeof(History_Diabetes)));
            this.Finish();
        }
    }
}