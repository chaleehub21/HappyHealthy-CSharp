using Android.App;
using Android.Widget;
using Android.OS;

namespace HappyHealthyCSharp
{
    [Activity(Label = "ProjectAppDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            SetContentView(Resource.Layout.activity_home);
        }
    }
}

