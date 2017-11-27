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
using Android.Support.V4.View;
using Android.Support.V7.AppCompat;
namespace HappyHealthyCSharp
{
    [Activity(Theme = "@style/MyMaterialTheme.Base",ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    class IntroHealthy : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_intro_healthy);
            ViewPager mViewPager = FindViewById<ViewPager>(Resource.Id.viewPageAndroid);
            AndroidImageAdapter adapter = new AndroidImageAdapter(this);
            mViewPager.Adapter = adapter;
        }
    }
}