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

namespace HappyHealthyCSharp
{
    [Activity(Label = "EmptyDemo")]
    public class EmptyDemo : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Empty);
            TTS DemoTTS = new TTS(this);
            DemoTTS.speak("ทดสอบเล่นเสียงหลังกดแบนเนอร์การแจ้งเตือน");
            // Create your application here
        }
    }
}