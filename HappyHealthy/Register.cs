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
//using Android.Icu.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Java.Text;

namespace HappyHealthyCSharp
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        DatePickerDialog mDatePicker;
        Calendar mCarlendar;
        string textDate, sysDate;
        TextView bdate;
        EditText email, pw, name, idNo;
        RadioButton mRadio, fRadio;
        ImageView register, backbtt;
        string insertDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            bdate = FindViewById<TextView>(Resource.Id.chooseBirthday);
            email = FindViewById<EditText>(Resource.Id.userID);
            pw = FindViewById<EditText>(Resource.Id.userPW);
            name = FindViewById<EditText>(Resource.Id.userName);
            mRadio = FindViewById<RadioButton>(Resource.Id.register_radio_male);
            fRadio = FindViewById<RadioButton>(Resource.Id.register_radio_female);
            register = FindViewById<ImageView>(Resource.Id.register_button);
            backbtt = FindViewById<ImageView>(Resource.Id.register_back_btt);
            //Code goes here
            mRadio.Checked = true;
            mCarlendar = Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("GMT+7"));
            bdate.Click += delegate {
                mDatePicker = new DatePickerDialog(this, delegate {
                    mCarlendar.Set(mDatePicker.DatePicker.Year, mDatePicker.DatePicker.Month, mDatePicker.DatePicker.DayOfMonth);
                    Date date = mCarlendar.Time;
                    textDate = new SimpleDateFormat("dd-MMMM-yyyy").Format(date);
                    insertDate = new SimpleDateFormat("yyyy-MM-dd").Format(date);
                    bdate.Text = textDate;

                }, 2000, DateTime.Now.Month, DateTime.Now.Day);
                mDatePicker.Show();
            };
            register.Click += delegate {
                if (isFieldValid())
                {
                    if(UserTABLE.InsertUserToSQL(name.Text, mRadio.Checked ? 'M' : 'F', insertDate, email.Text, pw.Text, this))
                    {
                        GlobalFunction.CreateDialogue(this, "การลงทะเบียนเสร็จสมบูรณ์ กลับไปยังหน้าเข้าใช้งาน", delegate
                        {
                            this.Finish();
                        }).Show();
                    }      
                }
            };
            // Create your application here
            backbtt.Click += delegate
            {
                this.Finish();
            };
        }
        public bool isFieldValid()
        {
            var emailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(pw.Text) || string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(insertDate))
            {
                Toast.MakeText(this, "Please fill all the required fields.", ToastLength.Long).Show();
                return false;
            }
            if (!Regex.IsMatch(email.Text, emailRegex)) { 
                Toast.MakeText(this, "Please fill the email in a valid form.", ToastLength.Long).Show();
                return false;
            }
            return true;
        }
    }
}