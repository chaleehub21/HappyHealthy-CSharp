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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.activity_report);
            PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            var fbs = FindViewById<RadioButton>(Resource.Id.report_fbs);
            var ckd = FindViewById<RadioButton>(Resource.Id.report_ckd);
            var bp = FindViewById<RadioButton>(Resource.Id.report_bp);
            
            fbs.Click += delegate {
                view.Model = CreatePlotModel("กราฟ FBS",new DiabetesTABLE().GetJavaList<DiabetesTABLE>($"SELECT * FROM DiabetesTABLE WHERE UD_ID = {Extension.getPreference("ud_id", 0, this)} ORDER BY FBS_TIME", new DiabetesTABLE().Column), "fbs_time", "fbs_fbs");
            };
            ckd.Click += delegate
            {
                view.Model = CreatePlotModel("กราฟ CKD", new KidneyTABLE().GetJavaList<KidneyTABLE>($@"SELECT * FROM KidneyTABLE WHERE UD_ID = {Extension.getPreference("ud_id", 0, this)} ORDER BY CKD_TIME",new KidneyTABLE().Column), "ckd_time", "ckd_gfr");
            };
            bp.Click += delegate {
                view.Model = CreatePlotModel("กราฟ BP", new PressureTABLE().GetJavaList<PressureTABLE>($@"SELECT * FROM PressureTABLE WHERE UD_ID = {Extension.getPreference("ud_id", 0, this)} ORDER BY BP_TIME",new PressureTABLE().Column), "bp_time", "bp_hr");
            };
            fbs.Checked = true;
            view.Model = CreatePlotModel("กราฟ FBS", new DiabetesTABLE().GetJavaList<DiabetesTABLE>($"SELECT * FROM DiabetesTABLE WHERE UD_ID = {Extension.getPreference("ud_id", 0, this)} ORDER BY FBS_TIME", new DiabetesTABLE().Column), "fbs_time", "fbs_fbs");
        }
        private PlotModel CreatePlotModel(string title,JavaList<IDictionary<string,object>> dataset,string key_time,string key_value)
        {
            var datalength = dataset.Count();
            var plotModel = new PlotModel { Title = title};
            object LastDateOnDataset = DateTime.Now;
            var maxValue = 0.0; //100.0;
            var minValue = 0.0;     
            if (datalength > 0)
            {
                dataset.Last().TryGetValue(key_time, out LastDateOnDataset);
            }        
            var startDate = DateTime.Parse(LastDateOnDataset.ToString()).AddDays(-15);
            var endDate = DateTime.Parse(LastDateOnDataset.ToString()).AddDays(15);
            var minDate = DateTimeAxis.ToDouble(startDate);
            var maxDate = DateTimeAxis.ToDouble(endDate);
            var x = new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minDate, Maximum = maxDate, MajorStep = 10, StringFormat = "d-MMMM" };
            var y = new LinearAxis { Position = AxisPosition.Left, Maximum = maxValue, Minimum = minValue };
            y.IsPanEnabled = false;
            y.IsZoomEnabled = false;
            plotModel.Axes.Add(x);
            plotModel.Axes.Add(y);
            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White,
                Color = OxyColors.Green,
            };
            for (var i = 0; i < datalength; i++)
            {
                dataset[i].TryGetValue(key_time, out object Time);
                dataset[i].TryGetValue(key_value, out object Value);
                var dCandidateValue = Convert.ToDouble(Value);
                if (dCandidateValue > maxValue)
                    maxValue = dCandidateValue;
                if (dCandidateValue < minValue)
                    minValue = dCandidateValue;
                DateTime.TryParse(Time.ToString(), out DateTime dateResult);
                double value = Convert.ToDouble(Value.ToString());
                series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateResult), value));
                var textAnnotations = new TextAnnotation() { TextPosition = new DataPoint(series1.Points.Last().X, series1.Points.Last().Y), Text = value.ToString(), Stroke = OxyColors.White };
                plotModel.Annotations.Add(textAnnotations);
            }
            #region Conclusion-Initial
            y.Minimum = minValue;
            y.Maximum = maxValue+5;
            if (maxValue > 100)
                series1.Color = OxyColors.Red;
            #endregion  
            plotModel.Series.Add(series1);
            return plotModel;
        }
    }
}