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
using OxyPlot.Annotations;

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
            view.Model = CreatePlotModel("กราฟ FBS",new DiabetesTABLE().getDiabetesList($"SELECT * FROM FBS WHERE UD_ID = {GlobalFunction.getPreference("ud_id","",this)} ORDER BY FBS_TIME"),"fbs_time","fbs_fbs");
            view2.Model = CreatePlotModel("กราฟ CKD",new KidneyTABLE().getKidneyList($"SELECT * FROM CKD WHERE UD_ID = {GlobalFunction.getPreference("ud_id","",this)} ORDER BY CKD_TIME"),"ckd_time","ckd_gfr");
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
        private PlotModel CreatePlotModel(string title,JavaList<IDictionary<string,object>> dataset,string key_time,string key_value)
        {
            var datalength = dataset.Count();
            var plotModel = new PlotModel { Title = title};
            //var x = new LinearAxis { Position = AxisPosition.Bottom };
            dataset.Last().TryGetValue(key_time, out object LastDateOnDataset);
            //var startDate = DateTime.Now.AddDays(-7);
            //var endDate = DateTime.Now();
            var startDate = DateTime.Parse(LastDateOnDataset.ToString()).AddDays(-7);
            var endDate = DateTime.Parse(LastDateOnDataset.ToString()).AddDays(0.1); 
            var minValue = DateTimeAxis.ToDouble(startDate);
            var maxValue = DateTimeAxis.ToDouble(endDate);
            Console.WriteLine($@"{minValue}/{maxValue}");
            var x = new DateTimeAxis { Position = AxisPosition.Bottom,Minimum = minValue,Maximum = maxValue,MajorStep = 10,StringFormat = "d-MMMM"};
            var y = new LinearAxis { Position = AxisPosition.Left, Maximum = 100, Minimum = 0 };
            y.IsPanEnabled = false;
            y.IsZoomEnabled = false;
            plotModel.Axes.Add(x);
            plotModel.Axes.Add(y);
            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White,
                Color = OxyColors.Red,
            };
            for(var i = 0; i < datalength; i++)
            {
                dataset[i].TryGetValue(key_time, out object Time);
                dataset[i].TryGetValue(key_value, out object Value);
                DateTime.TryParse(Time.ToString(), out DateTime dateResult);
                double value = Convert.ToDouble(Value.ToString());
                series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateResult.Year > 2100 ? dateResult.AddYears(-543) : dateResult), value));
                var textAnnotations = new TextAnnotation() {TextPosition = new DataPoint(series1.Points.Last().X, series1.Points.Last().Y),Text = value.ToString(),Stroke = OxyColors.White };
                plotModel.Annotations.Add(textAnnotations);
            }
            plotModel.Series.Add(series1);
            return plotModel;
        }
    }
}