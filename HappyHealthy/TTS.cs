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
using Java.Util;
using Android.Speech.Tts;
namespace HappyHealthyCSharp
{
    class TTS : UtteranceProgressListener, TextToSpeech.IOnInitListener, TextToSpeech.IOnUtteranceCompletedListener
    {
        public static TTS myTTS { get; set; }
        private Context context;
        private TextToSpeech tts;
        private Locale locale = Locale.Default;
        private string enginePackageName = "com.google.android.tts";
        private string message;
        private Boolean isRunning;
        private int speakCount;
        public TTS()
        {

        }
        public TTS(Context c)
        {
    
            this.context = c;
        }
        public static TTS GetInstance(Context c)
        {
            if(myTTS == null)
            {
                myTTS = new TTS(c);
            }
            return myTTS;
        }
        public void speak(string message)
        {
            this.message = message;
            if (tts == null || !isRunning)
            {
                speakCount = 0;
                if (enginePackageName != null && !string.IsNullOrEmpty(enginePackageName))
                {
                    tts = new TextToSpeech(context, this, enginePackageName);
                }
                else
                {
                    tts = new TextToSpeech(context, this);
                }
                if(Build.VERSION.SdkInt >= BuildVersionCodes.IceCreamSandwichMr1){
                    tts.SetOnUtteranceProgressListener(this);
                }
                else
                {
                    tts.SetOnUtteranceCompletedListener(this);
                }
                tts.SetPitch(1f);
                tts.SetSpeechRate(0.9f);
                isRunning = true;
            }
            else
            {
                startSpeak();
            }
        }

        private void startSpeak()
        {
            speakCount++;
            if(locale != null)
            {
                tts.SetLanguage(locale);
            }
            if(Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                tts.Speak(message, QueueMode.Flush, null, "");
            }
            else
            {
                tts.Speak(message, QueueMode.Flush, null);
            }
        }
        private void clear()
        {
            speakCount--;
            if (speakCount == 0)
            {
                tts.Shutdown();
                isRunning = false;
            }
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if(status == OperationResult.Success)
            {
                startSpeak();
            }
        }

        public void OnUtteranceCompleted(string utteranceId)
        {
            clear();
        }

        public override void OnDone(string utteranceId)
        {
            clear();
        }

        public override void OnError(string utteranceId)
        {
            clear();
        }

        public override void OnStart(string utteranceId)
        {
            //throw new NotImplementedException();
        }
    }
}