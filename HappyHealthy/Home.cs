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
    [Activity(Theme = "@style/MyMaterialTheme.Base")]
    class Home : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_home);
           
            
        }
        [Export("ClickFood")]
        public void ClickFood(View v)
        {
            StartActivity(new Intent(this, typeof(Food_Type_1)));
        }
        [Export("ClickExe")]
        public void ClickExe(View v)
        {
            StartActivity(new Intent(this, typeof(ExerciseType)));
        }
        [Export("ClickDiabetes")]
        public void ClickDiabetes(View v)
        {
            StartActivity(new Intent(this, typeof(Diabetes)));
        }
        [Export("ClickKidney")]
        public void ClickKidney(View v)
        {
            StartActivity(new Intent(this, typeof(Kidney)));
        }
        [Export("ClickPressure")]
        public void ClickPressure(View v)
        {
            StartActivity(new Intent(this, typeof(Pressure)));
        }
        [Export("ClickDevelop")]
        public void ClickDevelop(View v)
        {
            StartActivity(new Intent(this, typeof(Develop)));
        }
    }
}