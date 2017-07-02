using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using ProjectAppDemo;
using Android.Renderscripts;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Main Activity", MainLauncher = true, Icon = "@drawable/icon")]
    
    public class MainActivity : TabActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            SetContentView(Resource.Layout.activity_main);
            CreateTab(typeof(Home), "Home", "Home", Resource.Drawable.ic_home);
            CreateTab(typeof(Report),"Report", "Report", Resource.Drawable.ic_report);
            CreateTab(typeof(User), "FoodExe", "AlertHealthy", Resource.Drawable.ic_foodexe);
            CreateTab(typeof(IntroHealthy), "SaveDia", "Intro", Resource.Drawable.ic_heart);
            CreateTab(typeof(DisplayUser), "User", "User", Resource.Drawable.ic_user);
            #region FuckingComment
            /*
            var tabHost = FindViewById<TabHost>(Resource.Id.tabhost);                   
            var tab1 = tabHost.NewTabSpec("Home");
            tab1.SetIndicator("Home", Resources.GetDrawable(Resource.Drawable.ic_home));
            tab1.SetContent(new Intent(this, typeof(Home)).AddFlags(ActivityFlags.NewTask));        
            var tab2 = tabHost.NewTabSpec("Report");
            tab2.SetIndicator("Report", Resources.GetDrawable(Resource.Drawable.ic_report));
            tab2.SetContent(new Intent(this, typeof(Report)));
            var tab3 = tabHost.NewTabSpec("FoodExe");
            tab3.SetIndicator("AlertHealthy", Resources.GetDrawable(Resource.Drawable.ic_foodexe));
            tab3.SetContent(new Intent(this, typeof(User)));
            var tab4 = tabHost.NewTabSpec("SaveDia");
            tab4.SetIndicator("Intro", Resources.GetDrawable(Resource.Drawable.ic_heart));
            tab4.SetContent(new Intent(this, typeof(IntroHealthy)));
            var tab5 = tabHost.NewTabSpec("User");
            tab5.SetIndicator("User", Resources.GetDrawable(Resource.Drawable.ic_user));
            tab5.SetContent(new Intent(this, typeof(DisplayUser)));
            tabHost.AddTab(tab1);
            tabHost.AddTab(tab2);
            tabHost.AddTab(tab3);
            tabHost.AddTab(tab4);
            */
            #endregion
        }
        private void CreateTab(System.Type activityType, string tag, string label, int drawableId)
        {
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);

            var spec = TabHost.NewTabSpec(tag);
            var drawableIcon = Resources.GetDrawable(drawableId);
            spec.SetIndicator(label, drawableIcon);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }
    }
}

