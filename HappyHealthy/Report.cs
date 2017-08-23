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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

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
            SetContentView(Resource.Layout.activity_report);
            PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            PlotView view2 = FindViewById<PlotView>(Resource.Id.plot_view2);
            PlotView view3 = FindViewById<PlotView>(Resource.Id.plot_view3);
            PlotView view4 = FindViewById<PlotView>(Resource.Id.plot_view4);
            PlotView view5 = FindViewById<PlotView>(Resource.Id.plot_view5);
            view.Model = CreatePlotModel("กราฟ A");
            view2.Model = CreatePlotModel("กราฟ B");
            view3.Model = CreatePlotModel("กราฟ C");
            view4.Model = CreatePlotModel("กราฟ D");
            view5.Model = CreatePlotModel("กราฟ E");
            #region FullPagePlotView
            /*
            RequestWindowFeature(WindowFeatures.NoTitle);
            try
            {
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
                myClass = new MyClass("ทดสอบส่งพารามิเตอร์ label", float_Dataset);
                var plotView = new PlotView(this)
                {
                    Model = myClass.MyModel
                };
                AddContentView(plotView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            }
            catch
            {
                GlobalFunction.createDialog(this, "กรุณาตรวจสอบการเชื่อมต่ออินเทอร์เน็ต").Show();

            }
            
            
            //AddContentView(new PlotView(this) { Model = new MyClass().MyModel }, new ViewGroup.LayoutParams(200, 200));
        }
        protected override void OnPause()
        {
            base.OnPause();
        }
        */
            #endregion
        }
        private PlotModel CreatePlotModel(string title)
        {
            var diaTable = new DiabetesTABLE();
            var datalist = diaTable.getDiabetesList("SELECT * FROM FBS ORDER BY FBS_TIME");
            var datalength = datalist.Count();

            var plotModel = new PlotModel { Title = title };
            //var x = new LinearAxis { Position = AxisPosition.Bottom };
            var y = new LinearAxis { Position = AxisPosition.Left, Maximum = 100, Minimum = 0 };
            y.IsPanEnabled = false;
            y.IsZoomEnabled = false;
            //datetime axis
            var startDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now;
            var minValue = DateTimeAxis.ToDouble(startDate);
            var maxValue = DateTimeAxis.ToDouble(endDate);
            var x = new DateTimeAxis { Position = AxisPosition.Bottom,Minimum = minValue,Maximum = maxValue,StringFormat = "d-MMMM"};
            plotModel.Axes.Add(x);
            plotModel.Axes.Add(y);

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };
            var r = new System.Random();
            /*
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-7)), r.Next(0,10)));
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-6)), r.Next(0,10)));
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-5)), r.Next(0,10)));
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-4)), r.Next(0,10)));
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-3)), r.Next(0,10)));
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-2)), r.Next(0,10)));
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-1)), r.Next(0,10)));
            series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), r.Next(0, 10)));
            */
            for(var i = 0; i < datalength; i++)
            {
                datalist[i].TryGetValue("fbs_time", out object fbsTime);
                datalist[i].TryGetValue("fbs_fbs", out object fbsValue);
                DateTime.TryParse(fbsTime.ToString(), out DateTime dateResult);
                var value = Convert.ToDouble(fbsValue.ToString());
                series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateResult.Year > 2100?dateResult.AddYears(-543):dateResult), value));
            }
            plotModel.Series.Add(series1);
            System.Threading.Thread.Sleep(100);
            return plotModel;
        }
    }
}