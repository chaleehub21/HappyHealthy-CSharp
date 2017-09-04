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
using Java.Util;
using Android.Icu.Text;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        DatePickerDialog mDatePicker;
        Calendar mCarlendar;
        string textDate, sysDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            var bdate = FindViewById<TextView>(Resource.Id.chooseBirthday);
            var email = FindViewById<EditText>(Resource.Id.userID);
            var pw = FindViewById<EditText>(Resource.Id.userPW);
            var mRadio = FindViewById<RadioButton>(Resource.Id.register_radio_male);
            var fRadio = FindViewById<RadioButton>(Resource.Id.register_radio_female);
            var register = FindViewById<ImageView>(Resource.Id.register_btt);
            var regisTable = new RegisterTABLE();
            //Code go here
            mRadio.Checked = true;
            var insertDate = "";
            var dfm = new SimpleDateFormat("dd-MMMM-yyyy");
            mCarlendar = Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("GMT+7"));
            bdate.Click += delegate {
                mDatePicker = new DatePickerDialog(this, delegate {
                    mCarlendar.Set(mDatePicker.DatePicker.Year, mDatePicker.DatePicker.Month, mDatePicker.DatePicker.DayOfMonth);
                    Date date = mCarlendar.Time;
                    textDate = dfm.Format(date);
                    insertDate = new SimpleDateFormat("yyyy-MM-dd").Format(date);
                    bdate.Text = textDate;

                }, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                mDatePicker.Show();
            };
            register.Click += delegate {
                regisTable.InsertRegToSQL(regisTable.getLastestIDNO() + 1, "Chanvut Booneid", mRadio.Checked ? 'M' : 'F', insertDate, email.Text, pw.Text,this);
                //GlobalFunction.createDialog(this, "Show").Show();
            };
            // Create your application here
        }
    }
}