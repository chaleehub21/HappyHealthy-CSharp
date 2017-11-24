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
    class KidneyTABLE : DatabaseHelper
    {
        public override List<string> Column => new List<string>() {
            "ckd_id",
            "ckd_time",
            "ckd_time_string",
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
        [SQLite.Net.Attributes.PrimaryKey]
        public int ckd_id { get; set; }
        public DateTime ckd_time { get; set; }
        public string ckd_time_string { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_gfr { get; set; }
        [SQLite.Net.Attributes.MaxLength(4)]
        public int ckd_gfr_level { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_creatinine { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_bun { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_sodium { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_potassium { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_albumin_blood { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_albumin_urine { get; set; }
        [SQLite.Net.Attributes.MaxLength(3)]
        public decimal ckd_phosphorus_blood { get; set; }
        public int ud_id { get; set; }
        [SQLiteNetExtensions.Attributes.ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public KidneyTABLE()
        {
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
    }
}