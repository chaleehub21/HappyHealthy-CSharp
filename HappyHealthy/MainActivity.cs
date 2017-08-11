using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Renderscripts;
using System.IO;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Happy Healthy (Alpha)", MainLauncher = false, Icon = "@drawable/icon")]
    
    public class MainActivity : TabActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(bundle);
            //GlobalFunction.setPreference("dbPath", Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Database.DB_NAME), this);
            //GlobalFunction.dbPath = GlobalFunction.getPreference("dbPath", null, this);
            CreateTab(typeof(Home), "Home", "", Resource.Drawable.ic_home);
            CreateTab(typeof(Report), "Report", "", Resource.Drawable.ic_report);
            CreateTab(typeof(User), "AlertHealthy", "", Resource.Drawable.ic_foodexe);
            CreateTab(typeof(IntroHealthy), "Intro","" , Resource.Drawable.ic_heart);
            CreateTab(typeof(DisplayUser), "User", "", Resource.Drawable.ic_user);
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
            //intent.AddFlags(ActivityFlags.NewTask);
            intent.AddFlags(ActivityFlags.ClearTop);
            var spec = TabHost.NewTabSpec(tag);
            var drawableIcon = Resources.GetDrawable(drawableId);
            spec.SetIndicator(label, drawableIcon);
            spec.SetContent(intent);
            TabHost.AddTab(spec);
        }
    }
}

