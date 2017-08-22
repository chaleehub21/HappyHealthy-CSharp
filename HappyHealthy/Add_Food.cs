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
using Android.Support.V7.App;
using Java.Interop;

namespace HappyHealthyCSharp
{
    [Activity]
    public class Add_Food : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add__food);
            // Create your application here
        }
        [Export("ClickAddFoodBack")]
        public void ClickAddFoodBack(View v)
        {
            this.Finish();
        }
    }
}