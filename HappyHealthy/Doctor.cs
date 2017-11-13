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
using Java.Util;
using Android.Icu.Text;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Doctor")]
    public class Doctor : Activity
    {
        public struct App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }
        DatePickerDialog docDatePicker;
        Calendar docCarlendar;
        ImageView docAttendPicture;
        TextView docAttendDate, docRegisTime, docAppointmentTime;
        EditText et_docName, et_deptName, et_place, et_hospital, et_comment;
        DoctorTABLE docObject;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_doc);
            var camerabtt = FindViewById<ImageView>(Resource.Id.imageView_button_save_pic_doc);
            var backbtt = FindViewById<ImageView>(Resource.Id.imageView_button_back_doc);
            docAttendDate = FindViewById<TextView>(Resource.Id.choosedate_doc);
            docRegisTime = FindViewById<TextView>(Resource.Id.chooseregistime_doc);
            docAppointmentTime = FindViewById<TextView>(Resource.Id.chooseappttime_doc);
            et_docName = FindViewById<EditText>(Resource.Id.da_name);
            et_deptName = FindViewById<EditText>(Resource.Id.da_dept);
            et_place = FindViewById<EditText>(Resource.Id.da_place);
            et_hospital = FindViewById<EditText>(Resource.Id.da_hospital);
            et_comment = FindViewById<EditText>(Resource.Id.da_comment);
            var saveButton = FindViewById<ImageView>(Resource.Id.imageView_button_save_doc);
            //code goes below
            var flagObjectJson = Intent.GetStringExtra("targetObject") ?? string.Empty;
            docObject = string.IsNullOrEmpty(flagObjectJson) ? new DoctorTABLE() { da_reg_time = null } : JsonConvert.DeserializeObject<DoctorTABLE>(flagObjectJson);
            if (docObject.da_reg_time == null)
            {
                saveButton.Click += SaveValue;
            }
            else
            {
                InitialValueForUpdateEvent();
                saveButton.Click += UpdateValue;
            }
            backbtt.Click += delegate {
                this.Finish();
            };
            docAttendDate.Click += delegate {
                docCarlendar = Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("GMT+7"));
                docDatePicker = new DatePickerDialog(this, delegate {
                    docCarlendar.Set(docDatePicker.DatePicker.Year, docDatePicker.DatePicker.Month, docDatePicker.DatePicker.DayOfMonth);
                    Date date = docCarlendar.Time;
                    var textDate = new SimpleDateFormat("dd-MMMM-yyyy").Format(date);
                    docObject.da_date = Convert.ToDateTime(textDate).AddDays(1);
                    docAttendDate.Text = Convert.ToDateTime(textDate).ToThaiLocale().ToString("dd/MM/yyyy");
                }, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                docDatePicker.Show();
            };
            docRegisTime.Click += delegate {
                var tpickerFragment = TimePickerFragment.NewInstance(
                delegate (DateTime time)
                {
                    docRegisTime.Text = time.ToShortTimeString();
                });
                tpickerFragment.Show(FragmentManager, TimePickerFragment.TAG);
            };
            docAppointmentTime.Click+= delegate {
                var tpickerFragment = TimePickerFragment.NewInstance(
                delegate (DateTime time)
                {
                    docAppointmentTime.Text = time.ToShortTimeString();
                });
                tpickerFragment.Show(FragmentManager, TimePickerFragment.TAG);
            };
            if (IsAppToTakePicturesAvailable())
            {
                CreateDirForPictures();
                camerabtt.Click += cameraClickEvent;
                docAttendPicture = FindViewById<ImageView>(Resource.Id.imageView_show_image);
                //System.Console.WriteLine(IsAppToTakePicturesAvailable());
            }

            // Create your application here
        }

        private void InitialValueForUpdateEvent()
        {
            docAttendDate.Text = docObject.da_date.ToThaiLocale().ToString("dd/MM/yyyy");
            et_docName.Text = docObject.da_name;
            et_deptName.Text = docObject.da_dept;
            docRegisTime.Text = docObject.da_reg_time;
            docAppointmentTime.Text = docObject.da_appt_time;
            et_comment.Text = docObject.da_comment;
            et_place.Text = docObject.da_place;
            et_hospital.Text = docObject.da_hospital;
            //Waiting for image initialize
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            docObject.da_name = et_docName.Text;
            docObject.da_dept = et_deptName.Text;
            //docObject.da_date = DateTime.Parse(docAttendDate.Text).RevertThaiLocale();
            docObject.da_reg_time = docRegisTime.Text;
            docObject.da_appt_time = docAppointmentTime.Text;
            docObject.da_comment = et_comment.Text;
            docObject.da_pic = App._file != null ? App._file.AbsolutePath : docObject.da_pic;
            docObject.da_place = et_place.Text;
            docObject.da_hospital = et_hospital.Text;
            docObject.Update();
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
            docObject.da_name = et_docName.Text;
            docObject.da_dept = et_deptName.Text;
            docObject.da_reg_time = docRegisTime.Text;
            docObject.da_appt_time = docAppointmentTime.Text;
            docObject.da_comment = et_comment.Text;
            docObject.da_pic = picPath;
            docObject.da_place = et_place.Text;
            docObject.da_hospital = et_hospital.Text;
            docObject.Insert();
            //CustomNotification.SetAlarmManager(this, $"ได้เวลาทานยา {docObject.ma_name}", docObject.ma_set_time, Resource.Raw.notialert);
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
            var width = docAttendPicture.Width;
            App.bitmap = App._file.Path.LoadBitmap(width, height);//LoadBitmap(App._file.Path, width, height);
            if (App.bitmap != null)
            {
                docAttendPicture.SetImageBitmap(App.bitmap);
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
    public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        // TAG used for logging
        public static readonly string TAG = "MyTimePickerFragment";

        // Initialize handler to an empty delegate to prevent null reference exceptions:
        Action<DateTime> timeSelectedHandler = delegate { };

        // Factory method used to create a new TimePickerFragment:
        public static TimePickerFragment NewInstance(Action<DateTime> onTimeSelected)
        {
            // Instantiate a new TimePickerFragment:
            TimePickerFragment frag = new TimePickerFragment();

            // Set its event handler to the passed-in delegate:
            frag.timeSelectedHandler = onTimeSelected;

            // Return the new TimePickerFragment:
            return frag;
        }

        // Create and return a TimePickerDemo:
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Determine whether this activity uses 24-hour time format or not:
            bool is24HourFormat = Android.Text.Format.DateFormat.Is24HourFormat(Activity);

            //Uncomment to force 24-hour time format:
            is24HourFormat = true;

            // Instantiate a new TimePickerDemo, passing in the handler, the current 
            // time to display, and whether or not to use 24 hour format:
            TimePickerDialog dialog = new TimePickerDialog
                (Activity, this, currentTime.Hour, currentTime.Minute, is24HourFormat);

            // Return the created TimePickerDemo:
            return dialog;
        }

        // Called when the user sets the time in the TimePicker: 
        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            // Get the current time:
            DateTime currentTime = DateTime.Now;

            // Create a DateTime that contains today's date and the time selected by the user:
            DateTime selectedTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hourOfDay, minute, 0);
            // Invoke the handler to update the Activity's time display to the selected time:
            timeSelectedHandler(selectedTime);
        }
    }
}