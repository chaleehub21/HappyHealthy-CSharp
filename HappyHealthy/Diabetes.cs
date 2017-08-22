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

namespace HappyHealthyCSharp
{


    [Activity]
    public class Diabetes : Activity
    {
        ListView listview;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_diabetes);
            // Create your application here
        }
        [Export("ClickBackDiaHome")]
        public void ClickBackDiaHome(View v)
        {
            this.Finish();
        }
    }
}