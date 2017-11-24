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

namespace HappyHealthyCSharp
{
    class UserTABLE : DatabaseHelper
    {
        public override List<string> Column => throw new NotImplementedException();
        SQLitePlatformAndroid platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
        [SQLite.Net.Attributes.PrimaryKey]
        public int ud_id { get; set; }
        public string ud_iden_number { get; set; }
        public string ud_name { get; set; }
        public string ud_gender { get; set; }
        public DateTime ud_birthdate { get; set; }
        public DateTime ud_bf_time { get; set; }
        public DateTime ud_lu_time { get; set; }
        public DateTime ud_dn_time { get; set; }
        public DateTime ud_usually_meal_time { get; set; }
        [SQLite.Net.Attributes.Unique]
        public string ud_email { get; set; }
        public string ud_pass { get; set; }
        [OneToMany]
        public List<DiabetesTABLE> diabetesList { get; set; }
        [OneToMany]
        public List<KidneyTABLE> kidneyList { get; set; }
        [OneToMany]
        public List<MedicineTABLE> pillList { get; set; }
        [OneToMany]
        public List<PressureTABLE> pressureList { get; set; }
        public UserTABLE()
        {

        }
    }
}