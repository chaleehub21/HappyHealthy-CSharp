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
    
    class PressureTABLE : DatabaseHelper,IDatabaseSync
    {
        public override List<string> Column => new List<string>()
        {
            "bp_id",
            "bp_time",
            "bp_up",
            "bp_lo",
            "bp_hr",
            "bp_up_lvl",
            "bp_lo_lvl",
            "bp_hr_lvl",
        };
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int bp_id { get; set; }
        public DateTime bp_time { get; set; }
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
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public PressureTABLE()
        {

            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }

        public void Synchronize(Context c)
        {
            try
            {
                var syncThread = new Thread(() => {
                    var conn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
                    var mySQLConn = new MySqlConnection(Extension.remoteAccess);
                    mySQLConn.Open();
                    var MSCommand = mySQLConn.CreateCommand();
                    var result = conn.Query<TEMP_PressureTABLE>("SELECT * FROM Temp_PressureTABLE");
                    result.ForEach(row => {
                        if (row.mode == "I")
                        {
                            MSCommand.CommandText =
                            $"INSERT INTO ckd.PressureTABLE " +
                            $"values(" +
                            $"{row.bp_id_pointer}" +
                            $",'{row.bp_time_new.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                            $",{row.bp_up_new}" +
                            $",{row.bp_lo_new}" +
                            $",{row.bp_hr_new}" +
                            $",{row.bp_up_lvl_new}" +
                            $",{row.bp_lo_lvl_new}" +
                            $",{row.bp_hr_lvl_new}" +
                            $",{Extension.getPreference("ud_id", 0, c)}" +
                            $")";
                        }
                        else if (row.mode == "U")
                        {
                            MSCommand.CommandText =
                            $@"UPDATE ckd.PressureTABLE
                            SET
                               bp_time  = '{row.bp_time_new.ToString("yyyy-MM-dd HH:mm:ss")}'
                              ,bp_up    = {row.bp_up_new}
                              ,bp_lo    = {row.bp_lo_new}
                              ,bp_hr    = {row.bp_hr_new}
                              ,bp_up_lvl = {row.bp_up_lvl_new}
                              ,bp_lo_lvl = {row.bp_lo_lvl_new}
                              ,bp_hr_lvl = {row.bp_hr_lvl_new}
                            WHERE
                                bp_id = {row.bp_id_pointer}
                            AND
                                ud_id = {Extension.getPreference("ud_id",0,c)}
                        ";       
                        }
                        else if (row.mode == "D")
                        {
                            MSCommand.CommandText =
                            $@"DELETE FROM ckd.PressureTABLE where bp_id = {row.bp_id_pointer} AND ud_id = {Extension.getPreference("ud_id", 0, c)};";
                        }
                        Console.WriteLine(MSCommand.CommandText);
                        try
                        {
                            MSCommand.ExecuteNonQuery();
                        }
                        catch
                        {

                        }
                    });
                    mySQLConn.Close();
                    conn.DeleteAll<TEMP_PressureTABLE>();
                    conn.Close();
                });
                syncThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}