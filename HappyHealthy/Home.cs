﻿using System;
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
        }
        #endregion

        [Export("ClickFood")]
        public void ClickFood(View v)
        {
            Toast.MakeText(this, "Server is unavailable right at the moment, please try again later.", ToastLength.Short).Show();
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
        [Export("ClickDiabetes")]
        public void ClickDiabetes(View v)
        {
            StartActivity(new Intent(this, typeof(History_Diabetes)));
        }
        [Export("ClickKidney")]
        public void ClickKidney(View v)
        {
            StartActivity(new Intent(this, typeof(History_Kidney)));
        }
        [Export("ClickPressure")]
        public void ClickPressure(View v)
        {
            StartActivity(new Intent(this, typeof(History_Pressure)));
        }

        [Export("ClickDevelop")]
        public void ClickDevelop(View v)
        {
            //StartActivity(new Intent(this, typeof(Develop)));
            //GlobalFunction.createDialog(this, "Not implemented").Show();
           
        }
        [Export("ClickPill")]
        public void ClickPill(View v)
        {
            //GlobalFunction.createDialog(this, "Not implemented").Show();
            StartActivity(new Intent(this, typeof(History_Pill)));
        }
        [Export("ClickDoctor")]
        public void ClickDoctor(View v)
        {
            StartActivity(typeof(History_Doctor));
        }
    }
}