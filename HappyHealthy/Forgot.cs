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
    [Activity(Label = "Forgot")]
    public class Forgot : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forget_password);
            // Create your application here
            var backbtt = FindViewById<ImageView>(Resource.Id.forget_back_btt);
            backbtt.Click += delegate
            {
                this.Finish();
            };
        }
    }
}