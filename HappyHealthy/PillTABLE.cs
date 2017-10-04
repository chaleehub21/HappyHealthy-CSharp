using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace HappyHealthyCSharp
{
    class PillTABLE : DatabaseHelper
    {
        public readonly List<string> Column = new List<string> {
            "ma_id", 
            "ma_name",
            "ma_desc", 
            "ma_set_time",
            "ma_pic"
        };
        [PrimaryKey, AutoIncrement]
        public int ma_id { get; set; }
        public string ma_name { get; set; }
        public string ma_desc { get; set; }
        public DateTime ma_set_time { get; set; }
        public string ma_pic { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public PillTABLE()
        {
            var sqliteConn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<PillTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
    }

}