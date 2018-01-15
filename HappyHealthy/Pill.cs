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
using Newtonsoft.Json;

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
        EditText medName;
        EditText medDesc;
        MedicineTABLE medObject;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_pill);
            var camerabtt = FindViewById<ImageView>(Resource.Id.imageView_button_camera);
            var backbtt = FindViewById<ImageView>(Resource.Id.imageView_button_back_pill);
            medName = FindViewById<EditText>(Resource.Id.ma_name);
            medDesc = FindViewById<EditText>(Resource.Id.ma_desc);
            var saveButton = FindViewById<ImageView>(Resource.Id.imageView_button_save_pill);
            var deleteButton = FindViewById<ImageView>(Resource.Id.imageView_button_delete_pill);
            //code goes below
            var flagObjectJson = Intent.GetStringExtra("targetObject") ?? string.Empty;
            medObject = string.IsNullOrEmpty(flagObjectJson) ? new MedicineTABLE() { ma_name = string.Empty } : JsonConvert.DeserializeObject<MedicineTABLE>(flagObjectJson);
            if(medObject.ma_name == string.Empty)
            {
                saveButton.Click += SaveValue;
            }
            else
            {
                InitialValueForUpdateEvent();
                saveButton.Click += UpdateValue;
                deleteButton.Click += DeleteValue;
            }
            //end
            backbtt.Click += delegate {
                this.Finish();
            };
            if (IsAppToTakePicturesAvailable())
            {
                CreateDirForPictures();
                camerabtt.Click += cameraClickEvent;
                medImage = FindViewById<ImageView>(Resource.Id.imageView_show_image);
                //System.Console.WriteLine(IsAppToTakePicturesAvailable());
            }
            
            // Create your application here
        }

        private void DeleteValue(object sender, EventArgs e)
        {
            Extension.CreateDialogue(this, "Do you want to delete this value?", delegate
            {
                // Android.Net.Uri eventUri = Android.Net.Uri.Parse("content://com.android.calendar/events");
                //var deleteUri = ContentUris.WithAppendedId(eventUri, Convert.ToInt32(docObject.da_calendar_uri.Substring(docObject.da_calendar_uri.LastIndexOf(@"/") + 1)));
                var deleteUri = CalendarHelper.GetDeleteEventURI(medObject.ma_calendar_uri);
                ContentResolver.Delete(deleteUri, null, null);
                medObject.Delete<MedicineTABLE>(medObject.ma_id);
                Finish();
            }, delegate { }, "Yes", "No").Show();
        }

        private void InitialValueForUpdateEvent()
        {
            medName.Text = medObject.ma_name;
            medDesc.Text = medObject.ma_desc;
            //Waiting for image initialize
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            medObject.ma_name = medName.Text;
            medObject.ma_desc = medDesc.Text;
            medObject.ma_set_time = medObject.ma_set_time;
            medObject.ma_pic = App._file != null ? App._file.AbsolutePath : medObject.ma_pic;
            medObject.ud_id = Extension.getPreference("ud_id", 0, this);
            medObject.Update();
            Finish();
        }

        private void SaveValue(object sender, EventArgs e)
        {
            
            var picPath = string.Empty;
            if (App._file != null)
            {
                picPath = App._file.AbsolutePath;
            }
            //pillTable.InsertPillToSQL(medName.Text, medDesc.Text, DateTime.Now,picPath , GlobalFunction.getPreference("ud_id", "", this));
            medObject.ma_name = medName.Text;
            medObject.ma_desc = medDesc.Text;
            medObject.ma_set_time = DateTime.Now;
            medObject.ma_pic = picPath;
            medObject.ud_id = Extension.getPreference("ud_id", 0, this);
            medObject.Insert();
            var testRRULE = "FREQ=WEEKLY";
            var insertUri = CalendarHelper.GetEventContentValues(1, medName.Text, medDesc.Text, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 12, false,"UTC+7",testRRULE);
            var savedUri = ContentResolver.Insert(CalendarContract.Events.ContentUri, insertUri);
            medObject.ma_calendar_uri = savedUri.ToString();
            medObject.Update();
            //CustomNotification.SetAlarmManager(this, $"ได้เวลาทานยา {medObject.ma_name}",(int)DateTime.Now.DayOfWeek,medObject.ma_set_time, Resource.Raw.notialert);
            this.Finish();
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