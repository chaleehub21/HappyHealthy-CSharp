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
using SQLite.Net.Attributes;
using SQLite.Net;
using Xamarin.Forms.Platform.Android;
using System.Threading;

namespace HappyHealthyCSharp
{
    [Table("DiabetesTABLE")]
    class DiabetesTABLE : DatabaseHelper
    {
        [PrimaryKey,AutoIncrement]
        public int fbs_id { get; set; }
        public DateTime fbs_time { get; set; }
        public decimal fbs_fbs { get; set; }
        public int fbs_fbs_lvl { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public DiabetesTABLE()
        {
            var sqliteConn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            //var sqliteConn = new SQLiteConnection(GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<DiabetesTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
    }
}