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
using Java.IO;
using Android.Graphics;
using Android.Provider;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Pill")]
    public class Pill : Activity
    {
        public struct App {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }
        ImageView medImage;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_pill);
            var camerabtt = FindViewById<ImageView>(Resource.Id.cameraImageView);
            if (IsAppToTakePicturesAvailable())
            {
                CreateDirForPictures();
                camerabtt.Click += cameraClickEvent;
                medImage = FindViewById<ImageView>(Resource.Id.imageView1);
                //System.Console.WriteLine(IsAppToTakePicturesAvailable());
            }
            var addMed = FindViewById<ImageView>(Resource.Id.pill_save_button);
            var pillTable = new PillTABLE();
            addMed.Click += delegate {
                var medName = FindViewById<EditText>(Resource.Id.ma_name);
                var medDesc = FindViewById<EditText>(Resource.Id.ma_desc);
                var picPath = string.Empty;
                if(App._file != null)
                {
                    picPath = App._file.AbsolutePath;
                }
                pillTable.InsertPillToSQL(medName.Text, medDesc.Text, DateTime.Now,picPath , GlobalFunction.getPreference("ud_id", "", this));
                this.Finish();
            };
            // Create your application here
        }

        private void cameraClickEvent(object sender, EventArgs e)
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            //App._file = new File(App._dir, string.Format($@"HappyHealthyCS_{Guid.NewGuid()}.jpg"));
            var filePath = System.IO.Path.Combine(App._dir.AbsolutePath, $@"HappyHealthyCS_{Guid.NewGuid()}.jpg");
            App._file = new File(filePath);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            
            base.OnActivityResult(requestCode, resultCode, data);
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            var height = Resources.DisplayMetrics.HeightPixels;
            var width = medImage.Width;
            App.bitmap = App._file.Path.LoadBitmap(width, height);//LoadBitmap(App._file.Path, width, height);
            if(App.bitmap != null)
            {
                medImage.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }
            GC.Collect();
        }
        
        private void CreateDirForPictures()
        {
            App._dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "HappyHealthyCSharp");
            if (!App._dir.Exists())
                App._dir.Mkdirs();
        }
        private bool IsAppToTakePicturesAvailable()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            var availableActivities = PackageManager.QueryIntentActivities(intent, Android.Content.PM.PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
    }
}