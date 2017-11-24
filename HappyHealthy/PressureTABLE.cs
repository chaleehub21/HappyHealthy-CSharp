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
using System.Threading;
using System.Threading.Tasks;

namespace HappyHealthyCSharp
{

    class PressureTABLE : DatabaseHelper
    {
        public override List<string> Column => new List<string>()
        {
            "bp_id",
            "bp_time",
            "bp_time_string",
            "bp_up",
            "bp_lo",
            "bp_hr",
            "bp_up_lvl",
            "bp_lo_lvl",
            "bp_hr_lvl",
        };
        [SQLite.Net.Attributes.PrimaryKey]
        public int bp_id { get; set; }
        public DateTime bp_time { get; set; }
        public string bp_time_string { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal bp_up { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal bp_lo { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public int bp_hr { get; set; }
        [SQLite.Net.Attributes.MaxLength(4)]
        public int bp_up_lvl { get; set; }
        [SQLite.Net.Attributes.MaxLength(4)]
        public int bp_lo_lvl { get; set; }
        [SQLite.Net.Attributes.MaxLength(4)]
        public int bp_hr_lvl { get; set; }
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public PressureTABLE()
        {

            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
    }
}