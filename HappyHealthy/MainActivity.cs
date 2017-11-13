//comment
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
            CreateTab(typeof(UserDetail), "User", "", Resource.Drawable.ic_user);
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

