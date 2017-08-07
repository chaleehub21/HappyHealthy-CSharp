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
using Java.Text;
using OxyPlot.Xamarin.Android;
using MySql.Data.MySqlClient;
using System.Data;

namespace HappyHealthyCSharp
{
    
    [Activity(Theme = "@style/MyMaterialTheme.Base")]
    
    class Report : Activity
    {
        #region deprecation_code
        /*
        DatePickerDialog mDatePicker;
        Calendar mCarlendar;
        Calendar c;
        SimpleDateFormat df_show, dfm, dfm_insert;
        string sysDate;
        string textDate;
        TextView chooseDate, totalCal, totalFood, totalExe, protein, carbo, fat, sugar, sodium;
        FoodHistoryTABLE foodHistoryTable;
        Dictionary<string, string> data;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //SetTheme(Resource.Style.Base_Theme_AppCompat_Light);
            SetContentView(Resource.Layout.activity_report);
            dfm = new SimpleDateFormat("dd-MMMM-yyyy");
            dfm_insert = new SimpleDateFormat("yyyy-MM-dd");
            c = Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("GMT+7"));
            sysDate = dfm_insert.Format(c.Time) + "%";
            textDate = dfm.Format(c.Time);
            mCarlendar = Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("GMT+7"));
            chooseDate = FindViewById<TextView>(Resource.Id.chooseDate);
            totalCal = FindViewById<TextView>(Resource.Id.totalCal);
            totalFood = FindViewById<TextView>(Resource.Id.tv_sum_food_cal);
            totalExe = FindViewById<TextView>(Resource.Id.tv_sum_ex_cal);
            protein = FindViewById<TextView>(Resource.Id.tv_sum_pro);
            carbo = FindViewById<TextView>(Resource.Id.tv_sum_car);
            fat = FindViewById<TextView>(Resource.Id.tv_sum_fat);
            sugar = FindViewById<TextView>(Resource.Id.tv_sum_sugar);
            sodium = FindViewById<TextView>(Resource.Id.tv_sum_sodium);
            foodHistoryTable = new FoodHistoryTABLE();
            setvalue(sysDate);
            chooseDate.Click += delegate {
                mDatePicker = new DatePickerDialog(this,delegate {
                    mCarlendar.Set(mDatePicker.DatePicker.Year,mDatePicker.DatePicker.Month,mDatePicker.DatePicker.DayOfMonth);
                    Date date = mCarlendar.Time;
                    textDate = dfm.Format(date);
                    sysDate = dfm_insert.Format(date) + "%";
                    setvalue(sysDate);
                }, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                mDatePicker.Show();
            };
            
        }
        public void setvalue(string dateChoose)
        {
            chooseDate.SetText(textDate.ToCharArray(), 0, textDate.Length);
        }
        */
        #endregion

        public MyClass myClass;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            var sqlconn = new MySqlConnection(GlobalFunction.remoteaccess);
            sqlconn.Open();
            var query = $@"SELECT food_carbohydrate FROM Food";
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            var float_Dataset = new List<double>();
            adapter.Fill(tickets, "Food");
            foreach (DataRow x in tickets.Tables["Food"].Rows)
            {
                float_Dataset.Add(Convert.ToDouble(x[0].ToString()));
            }
            myClass = new MyClass("ทดสอบส่งพารามิเตอร์ label",float_Dataset);
            var plotView = new PlotView(this)
            {
                Model = myClass.MyModel
            };
            AddContentView(plotView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            //AddContentView(new PlotView(this) { Model = new MyClass().MyModel }, new ViewGroup.LayoutParams(200, 200));
        }
    }
    
}