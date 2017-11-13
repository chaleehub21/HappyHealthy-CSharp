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
using SQLite;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms.Platform.Android;
using System.Threading;
using SQLite.Net.Attributes;

namespace HappyHealthyCSharp
{
    [SQLite.Net.Attributes.Table("DiabetesTABLE")]
    class DiabetesTABLE : DatabaseHelper
    {
        public override List<string> Column => new List<string>()
        {
            "fbs_id",
            "fbs_time",
            "fbs_fbs",
            "fbs_fbs_lvl",
            "ud_id"
        };
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int fbs_id { get; set; }
        public DateTime fbs_time { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal fbs_fbs { get; set; }
        [SQLite.Net.Attributes.MaxLength(4)]
        public int fbs_fbs_lvl { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }

        //reconstruct of sqlite keys + attributes
        public DiabetesTABLE()
        {
            
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
    }
}