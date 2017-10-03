﻿using System;
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
using System.Threading;

namespace HappyHealthyCSharp
{
    class KidneyTABLE : DatabaseHelper
    {
        public readonly List<string> Column = new List<string>() {
            "ckd_id", 
            "ckd_time",
            "ckd_gfr",
            "ckd_gfr_level", 
            "ckd_creatinine", 
            "ckd_bun",
            "ckd_sodium", 
            "ckd_potassium", 
            "ckd_albumin_blood", 
            "ckd_albumin_urine",
            "ckd_phosphorus_blood"
        };
        [PrimaryKey, AutoIncrement]
        public int ckd_id { get; set; }
        public DateTime ckd_time { get; set; }
        public decimal ckd_gfr { get; set; }
        public int ckd_gfr_level { get; set; }
        public decimal ckd_creatinine { get; set; }
        public decimal ckd_bun { get; set; }
        public decimal ckd_sodium { get; set; }
        public decimal ckd_potassium { get; set; }
        public decimal ckd_albumin_blood { get; set; }
        public decimal ckd_albumin_urine { get; set; }
        public decimal ckd_phosphorus_blood { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public KidneyTABLE()
        {
            var sqliteConn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<KidneyTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
    }
}