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
    [Activity(Label = "Pill")]
    public class History_Pill : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_history_pill);
            // Create your application here
            var backbtt = FindViewById<ImageView>(Resource.Id.imageViewbackpill);
            backbtt.Click += delegate {
                this.Finish();
            };
            var addbtt = FindViewById<ImageView>(Resource.Id.imageViewAddPill);
            addbtt.Click += delegate
            {
                StartActivity(typeof(Pill));
            };
        }
    }
}