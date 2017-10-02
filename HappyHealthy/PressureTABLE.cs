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
using SQLite.Net.Attributes;
using SQLite.Net;

namespace HappyHealthyCSharp
{
    class PressureTABLE
    {
        [PrimaryKey,AutoIncrement]
        public int bp_id { get; set; }
        public DateTime bp_time { get; set; }
        public decimal bp_up { get; set; }
        public decimal bp_lo { get; set; }
        public int bp_hr { get; set; }
        public int bp_up_lvl { get; set; }
        public int bp_lo_lvl { get; set; }
        public int bp_hr_lvl { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public PressureTABLE()
        {
            var sqliteConn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<PressureTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getPressureList(string queryCustomized = "SELECT * FROM PressureTABLE")
        {
            //conn = new SQLiteConnection(GlobalFunction.dbPath);
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var bpList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "BP");
            foreach(DataRow x in tickets.Tables["BP"].Rows)
            {
                var bp = new JavaDictionary<string, object>();
                bp.Add("bp_id", GlobalFunction.StringValidation(x[0].ToString()));
                bp.Add("bp_time", GlobalFunction.StringValidation(x[1].ToString()));
                bp.Add("bp_up", GlobalFunction.StringValidation(x[2].ToString()));
                bp.Add("bp_lo", GlobalFunction.StringValidation(x[3].ToString()));
                bp.Add("bp_hr", GlobalFunction.StringValidation(x[4].ToString()));
                bp.Add("bp_up_lvl", GlobalFunction.StringValidation(x[5].ToString()));
                bp.Add("bp_lo_lvl", GlobalFunction.StringValidation(x[6].ToString()));
                bp.Add("bp_hr_lvl", GlobalFunction.StringValidation(x[7].ToString()));
                bpList.Add(bp);
            }
            sqlconn.Close();*/
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            var bpList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var backFromSQL = conn.Query<PressureTABLE>(query);
            backFromSQL.ForEach(x =>
            {
                var bp = new JavaDictionary<string, object>();
                bp.Add("bp_id", GlobalFunction.StringValidation(x.bp_id.ToString()));
                bp.Add("bp_time", GlobalFunction.StringValidation(x.bp_time.ToLocalTime().ToString()));
                bp.Add("bp_up", GlobalFunction.StringValidation(x.bp_up.ToString()));
                bp.Add("bp_lo", GlobalFunction.StringValidation(x.bp_lo.ToString()));
                bp.Add("bp_hr", GlobalFunction.StringValidation(x.bp_hr.ToString()));
                bp.Add("bp_up_lvl", GlobalFunction.StringValidation(x.bp_up_lvl.ToString()));
                bp.Add("bp_lo_lvl", GlobalFunction.StringValidation(x.bp_lo_lvl.ToString()));
                bp.Add("bp_hr_lvl", GlobalFunction.StringValidation(x.bp_hr_lvl.ToString()));
                bpList.Add(bp);
            });
            conn.Close();
            return bpList;
        }
        public void deletePressureFromSQL(string id)
        {
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM BP WHERE bp_id = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
            */
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            conn.Delete<PressureTABLE>(id);
            conn.Close();
        }
        public void InsertPressureToSQL(string up,string low,string heart_rate,int userID)
        {
            #region deprecated
            /*
            var conn = new SQLiteAsyncConnection(GlobalFunction.dbPath);
            await conn.CreateTableAsync'FoodTABLE'();
            int retRecord = await conn.InsertAsync(foodinstance);
            */
            #endregion
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = $@"INSERT INTO BP
                                        VALUES
                                        (null,
                                        '{DateTime.Now.ToThaiLocale().ToString("yyyy-MM-dd H:mm:ss")}',
                                        {up},
                                        {low},
                                        {heart_rate},
                                        {up },
                                        {low},
                                        {heart_rate},
                                        {userID});";
            Console.WriteLine($@"INSERT INTO BP VALUES(null,'{DateTime.Now.ToThaiLocale().ToString("yyyy-MM-dd H:mm:ss")}',{up},{low},{heart_rate},{up },{low},{heart_rate},{userID});");
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
        public void InsertPressureToSQL(PressureTABLE data)
        {
            var conn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            conn.Insert(data);
            try
            {
                InsertPressureToSQL(data.bp_up, data.bp_lo, data.bp_hr, data.ud_id);
            }
            catch
            {
                //pass
            }
            conn.Close();
        }

        private void InsertPressureToSQL(decimal bp_up, decimal bp_lo, int bp_hr, int ud_id)
        {
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            decimal bp_up_lvl, bp_lo_lvl, bp_hr_lvl;
            bp_up_lvl = 0;
            bp_lo_lvl = 0;
            bp_hr_lvl = 0;
            sqlCommand.CommandText = $@"INSERT INTO BP
                                        VALUES
                                        (null,
                                        '{DateTime.Now.ToThaiLocale().ToString("yyyy-MM-dd H:mm:ss")}',
                                        {bp_up},
                                        {bp_lo},
                                        {bp_hr},
                                        {bp_up_lvl},
                                        {bp_lo_lvl},
                                        {bp_hr_lvl},
                                        {ud_id});";
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}