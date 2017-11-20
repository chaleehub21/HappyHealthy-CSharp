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
    class KidneyTABLE : DatabaseHelper,IDatabaseSync
    {
        public override List<string> Column => new List<string>() {
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
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int ckd_id { get; set; }
        public DateTime ckd_time { get; set; }
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
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [SQLiteNetExtensions.Attributes.ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public KidneyTABLE()
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
                    var result = conn.Query<TEMP_KidneyTABLE>("SELECT * FROM TEMP_KidneyTABLE");
                    result.ForEach(row => {
                        if (row.mode == "I")
                        {
                            MSCommand.CommandText = $"" +
                            $"INSERT INTO ckd.KidneyTABLE " +
                            $"values(" +
                            $"{row.ckd_id_pointer}" +
                            $",'{row.ckd_time_new.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                            $",{row.ckd_gfr_new}" +
                            $",{row.ckd_gfr_level_new}" +
                            $",{row.ckd_creatinine_new}" +
                            $",{row.ckd_bun_new}"+
                            $",{row.ckd_sodium_new}"+
                            $",{row.ckd_potassium_new}"+
                            $",{row.ckd_albumin_blood_new}"+
                            $",{row.ckd_albumin_urine_new}"+
                            $",{row.ckd_phosphorus_blood_new}"+
                            $",{Extension.getPreference("ud_id", 0, c)})";
                        }
                        else if (row.mode == "U")
                        {
                            MSCommand.CommandText =
                            $@"UPDATE ckd.KidneyTABLE 
                        SET
                            ckd_time        = '{row.ckd_time_new.ToString("yyyy-MM-dd HH:mm:ss")}'
                            ,ckd_gfr        = {row.ckd_gfr_new}
                            ,ckd_gfr_level  = {row.ckd_gfr_level_new}
                            ,ckd_creatinine = {row.ckd_creatinine_new}
                            ,ckd_bun        = {row.ckd_bun_new}
                            ,ckd_sodium     = {row.ckd_sodium_new}
                            ,ckd_potassium  = {row.ckd_potassium_new}
                            ,ckd_albumin_blood = {row.ckd_albumin_blood_new}
                            ,ckd_albumin_urine = {row.ckd_albumin_urine_new}
                            ,ckd_phosphorus_blood = {row.ckd_phosphorus_blood_new}
                        WHERE 
                            ckd_id = {row.ckd_id_pointer}
                        AND
                            ud_id = {Extension.getPreference("ud_id", 0, c)};
                        ";
                        }
                        else if (row.mode == "D")
                        {
                            MSCommand.CommandText =
                            $@"DELETE FROM ckd.KidneyTABLE where ckd_id = {row.ckd_id_pointer} AND ud_id = {Extension.getPreference("ud_id", 0, c)};";
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
                    conn.DeleteAll<TEMP_KidneyTABLE>();
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