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
using Android.Graphics.Drawables;
using static Android.Support.V4.View.ViewPropertyAnimatorCompat;
using Java.Lang;
using Android.Views.Animations;
using Android.Speech.Tts;

namespace HappyHealthyCSharp
{
    [Activity(Theme = "@style/MyMaterialTheme.Base", MainLauncher = true,NoHistory = true)]
    public class SplashScreen : Activity,TextToSpeech.IOnInitListener
    {
        TextToSpeech textToSpeech;
        private readonly int CheckCode = 101, NeedLang = 103;
        Java.Util.Locale lang;
        ImageView imageView;
        Animation view_animation;
        TTS t2sEngine;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var en_USLocale = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = en_USLocale;
            t2sEngine = new TTS(this);
            new UserTABLE();
            new DiabetesTABLE();
            new KidneyTABLE();
            new PressureTABLE();
            new FoodTABLE();
            
        }
        public override void OnBackPressed() { } //override back button to prevent loading process cancellation
        protected override void OnResume()
        {
            base.OnResume();
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.activity_splash_screen);
            imageView = FindViewById<ImageView>(Resource.Id.imageView9);
            view_animation = AnimationUtils.LoadAnimation(this, Resource.Animation.fade_in);
            imageView.StartAnimation(view_animation);
            view_animation.AnimationEnd += delegate {
                t2sEngine.speak("Hello World!");
                StartActivity(typeof(Login));
            };
        }
        #region Experiment TTS methods
        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (status == OperationResult.Error)
                textToSpeech.SetLanguage(Java.Util.Locale.Default);
            // if the listener is ok, set the lang
            if (status == OperationResult.Success)
                textToSpeech.SetLanguage(lang);
        }
        protected override void OnActivityResult(int req, Result res, Intent data)
        {
            if (req == NeedLang)
            {
                // we need a new language installed
                var installTTS = new Intent();
                installTTS.SetAction(TextToSpeech.Engine.ActionInstallTtsData);
                StartActivity(installTTS);
            }
        }
#endregion
    }
}