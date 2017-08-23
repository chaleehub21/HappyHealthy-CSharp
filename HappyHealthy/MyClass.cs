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

namespace HappyHealthyCSharp
{
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    class MyClass
    {
        /// <summary>
		/// Gets or sets the plot model that is shown in the demo apps.
		/// </summary>
		/// <value>My model.</value>
		public PlotModel MyModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OxyPlotSample.MyClass"/> class.
        /// </summary>
        public MyClass(string label,List<double> dataset_y)
        {
            var plotModel = new PlotModel
            {
                Title = label
            };

            var xaxis = new LinearAxis
            {
                Position = AxisPosition.Bottom
            };

            var yaxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = findMin<double>(dataset_y),
                Maximum = findMax<double>(dataset_y)
                
            };

            plotModel.Axes.Add(xaxis);
            plotModel.Axes.Add(yaxis);
            var series1 = new LineSeries
            {
                StrokeThickness = 3,
                MarkerType = MarkerType.None,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Blue,
                MarkerStrokeThickness = 1,


            };       
            for (var i = 0; i < dataset_y.Count; i++)
            {
                series1.Points.Add(new DataPoint(i, dataset_y[i]));
            }
            plotModel.Series.Add(series1);

            this.MyModel = plotModel;
        }
        public T findMax<T>(List<T> dataset)
        {
            return dataset.Max();
        }
        public T findMin<T>(List<T> dataset)
        {
            return dataset.Min();
        }
    }
}