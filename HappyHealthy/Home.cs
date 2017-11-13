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
            #region Speech-To-Text Implementation
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
            #endregion
           
        }

        public void ClickFood(object sender, EventArgs e)
        {
            if (MySQLDatabaseHelper.TestConnection(Extension.remoteAccess) == true)
            {
                StartActivity(new Intent(this, typeof(History_Food)));
            }
            else
            {
                Toast.MakeText(this, "Server is unavailable right at the moment, please try again later.", ToastLength.Short).Show();
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
            Extension.CreateDialogue(this, "Not implemented").Show();
        }
    }
}