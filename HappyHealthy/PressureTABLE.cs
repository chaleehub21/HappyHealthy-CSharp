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
        [SQLite.PrimaryKey]
        public int bp_id { get; set; }
        public DateTime bp_time { get; set; }
        public string bp_time_string { get; set; }
        [SQLite.MaxLength(3)]
        public decimal bp_up { get; set; }
        [SQLite.MaxLength(3)]
        public decimal bp_lo { get; set; }
        [SQLite.MaxLength(3)]
        public int bp_hr { get; set; }
        [SQLite.MaxLength(4)]
        public int bp_up_lvl { get; set; }
        [SQLite.MaxLength(4)]
        public int bp_lo_lvl { get; set; }
        [SQLite.MaxLength(4)]
        public int bp_hr_lvl { get; set; }
        public int ud_id { get; set; }
        [Ignore]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public PressureTABLE()
        {

            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public static void TrySyncWithMySQL(Context c)
        {
            var t = new Thread(() =>
            {
                try
                {
                    var Service = new HHCSService.HHCSService();
                    var presList = new List<HHCSService.TEMP_PressureTABLE>();
                    new TEMP_PressureTABLE().Select<TEMP_PressureTABLE>($"SELECT * FROM TEMP_PressureTABLE WHERE ud_id = '{Extension.getPreference("ud_id", 0, c)}'").ForEach(row =>
                    {
                        var wsObject = new HHCSService.TEMP_PressureTABLE();
                        wsObject.bp_id_pointer = row.bp_id_pointer;
                        wsObject.bp_time_new = row.bp_time_new;
                        wsObject.bp_time_old = row.bp_time_old;
                        wsObject.bp_time_string_new = row.bp_time_string_new;
                        wsObject.bp_up_new = row.bp_up_new;
                        wsObject.bp_up_old = row.bp_up_old;
                        wsObject.bp_lo_new = row.bp_lo_new;
                        wsObject.bp_lo_old = row.bp_lo_old;
                        wsObject.bp_hr_new = row.bp_hr_new;
                        wsObject.bp_hr_old = row.bp_hr_old;
                        wsObject.bp_up_lvl_new = row.bp_up_lvl_new;
                        wsObject.bp_up_lvl_old = row.bp_up_lvl_old;
                        wsObject.bp_lo_lvl_new = row.bp_lo_lvl_new;
                        wsObject.bp_lo_lvl_old = row.bp_lo_lvl_old;
                        wsObject.bp_hr_lvl_new = row.bp_hr_lvl_new;
                        wsObject.bp_hr_lvl_old = row.bp_hr_lvl_old;
                        wsObject.mode = row.mode;
                        presList.Add(wsObject);
                    });
                    Service.SynchonizeData(Extension.getPreference("ud_email", string.Empty,c)
                        , Extension.getPreference("ud_pass", string.Empty, c)
                        , new List<HHCSService.TEMP_DiabetesTABLE>().ToArray()
                        , new List<HHCSService.TEMP_KidneyTABLE>().ToArray()
                        , presList.ToArray());
                    presList.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
            t.Start();
        }
    }
}