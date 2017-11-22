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
using Xamarin.Forms.Platform.Android;
using System.Threading;
using SQLite.Net.Attributes;
using System.Threading.Tasks;

namespace HappyHealthyCSharp
{
    [SQLite.Net.Attributes.Table("DiabetesTABLE")]
    class DiabetesTABLE : DatabaseHelper, IDatabaseSync
    {
        public override List<string> Column => new List<string>()
        {
            "fbs_id",
            "fbs_time",
            "fbs_time_string",
            "fbs_fbs",
            "fbs_fbs_lvl",
            "ud_id"
        };
        [SQLite.Net.Attributes.PrimaryKey]
        public int fbs_id { get; set; }
        public DateTime fbs_time { get; set; }
        public string fbs_time_string { get; set; }
        public decimal fbs_fbs { get; set; }
        public int fbs_fbs_lvl { get; set; }
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }

        //reconstruct of sqlite keys + attributes
        public DiabetesTABLE()
        {

            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }

        public async void SynchronizeDataAsync(Context c)
        {
            object threadResult = null;
            await Task.Run(() =>
            {
                try
                {
                    var conn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), Extension.sqliteDBPath);
                    var mySQLConn = new MySqlConnection(Extension.remoteAccess);
                    mySQLConn.Open();
                    var MSCommand = mySQLConn.CreateCommand();
                    var result = conn.Query<TEMP_DiabetesTABLE>("SELECT * FROM Temp_DiabetesTABLE");
                    result.ForEach(row =>
                    {
                        if (row.mode == "I")
                        {
                            MSCommand.CommandText =
                            $"INSERT INTO ckd.DiabetesTABLE " +
                            $"values({row.fbs_id_pointer}" +
                            $",'{row.fbs_time_new.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                            $",{row.fbs_fbs_new}" +
                            $",{row.fbs_fbs_lvl_new}" +
                            $",{Extension.getPreference("ud_id", 0, c)})";
                        }
                        else if (row.mode == "U")
                        {
                            MSCommand.CommandText =
                            $@"UPDATE ckd.DiabetesTABLE 
                                SET
                                    fbs_fbs = {row.fbs_fbs_new}
                                    ,fbs_time = '{row.fbs_time_string_new}'
                                    ,fbs_fbs_lvl = {row.fbs_fbs_lvl_new}
                                WHERE 
                                    fbs_id = {row.fbs_id_pointer}
                                AND
                                    ud_id = {Extension.getPreference("ud_id", 0, c)};
                                ";
                        }
                        else if (row.mode == "D")
                        {
                            MSCommand.CommandText =
                            $@"DELETE FROM ckd.DiabetesTABLE where fbs_id = {row.fbs_id_pointer} AND ud_id = {Extension.getPreference("ud_id", 0, c)};";
                        }
                        Console.WriteLine(MSCommand.CommandText);
                        try
                        {
                            MSCommand.ExecuteNonQuery();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    });
                    mySQLConn.Close();
                    conn.DeleteAll<TEMP_DiabetesTABLE>();
                    conn.Close();
                    threadResult = true;
                }
                catch
                {
                    threadResult = false;
                }
            });
            if ((bool)threadResult == false)
            {
                Extension.CreateDialogue(c, "Unable to push data to server").Show();
            }
        }
    }
}