﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using SQLite;
using SQLite.Net.Platform.XamarinAndroid;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace HappyHealthyCSharp
{
    class DoctorTABLE : DatabaseHelper
    {
        public override List<string> Column => new List<string> {
            "da_id",
            "da_date",
            "da_pic",
            "da_name",
            "da_dept",
            "da_reg_time",
            "da_appt_time",
            "da_calendar_id",
            "da_comment",
            "da_place",
            "da_hospital"
        };
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int da_id { get; set; }
        [SQLite.Net.Attributes.MaxLength(100)]
        public DateTime da_date { get; set; }
        [SQLite.Net.Attributes.MaxLength(255)]
        public string da_pic { get; set; }
        [SQLite.Net.Attributes.MaxLength(255)]
        public string da_name { get; set; }
        [SQLite.Net.Attributes.MaxLength(255)]
        public string da_dept { get; set; }
        public string da_reg_time { get; set; }
        public string da_appt_time { get; set; }
        public int da_calendar_id { get; set; }
        [SQLite.Net.Attributes.MaxLength(255)]
        public string da_comment { get; set; }
        [SQLite.Net.Attributes.MaxLength(255)]
        public string da_place { get; set; }
        [SQLite.Net.Attributes.MaxLength(255)]
        public string da_hospital { get; set; }
        //reconstruct of sqlite keys + attributes
        public DoctorTABLE()
        {

            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
    }

}