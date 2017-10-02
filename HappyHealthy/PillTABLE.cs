using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace HappyHealthyCSharp
{
    class PillTABLE
    {
        [PrimaryKey, AutoIncrement]
        public int ma_id { get; set; }
        public string ma_name { get; set; }
        public string ma_desc { get; set; }
        public DateTime ma_set_time { get; set; }
        public string ma_pic { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public PillTABLE()
        {
            var sqliteConn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<PillTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getPillList(string queryCustomized = "SELECT * FROM PillTABLE")
        {
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var pillList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "MED_ALERT");
            foreach(DataRow x in tickets.Tables["MED_ALERT"].Rows)
            {
                var pill = new JavaDictionary<string, object>();
                pill.Add("ma_id", x[0].ToString().StringValidation());
                pill.Add("ma_name", x[1].ToString().StringValidation());
                pill.Add("ma_desc", x[2].ToString().StringValidation());
                pill.Add("ma_set_time", x[3].ToString().StringValidation());
                pill.Add("ma_pic", x[4].ToString().StringValidation());
                pillList.Add(pill);
            }
            sqlconn.Close();
            */
            var pillList = new JavaList<IDictionary<string, object>>();
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            var query = queryCustomized;
            var backFromSQL = conn.Query<PillTABLE>(query);
            backFromSQL.ForEach(x =>
            {
                var pill = new JavaDictionary<string, object>();
                pill.Add("ma_id", x.ma_id.ToString().StringValidation());
                pill.Add("ma_name", x.ma_name.ToString().StringValidation());
                pill.Add("ma_desc", x.ma_desc.ToString().StringValidation());
                pill.Add("ma_set_time", x.ma_set_time.ToLocalTime().ToString().StringValidation());
                pill.Add("ma_pic", x.ma_pic.ToString().StringValidation());
                pillList.Add(pill);
            });
            conn.Close();
            return pillList;
        }
        public void deletePillFromSQL(string id)
        {
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM MED_ALERT WHERE MA_ID = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
            */
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            conn.Delete<PillTABLE>(id);
            conn.Close();
        }
        public void InsertPillToSQL(string medName,string medDesc,DateTime medTime,string picPath,string userID)
        {
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = $@"insert into med_alert values(null,'{medName}','{medDesc}','{medTime.ToThaiLocale().ToString("H:mm:ss")}','{picPath}',{userID})";
            sqlCommand.ExecuteNonQuery();
            conn.Close();

        }
        public void InsertPillToSQL(PillTABLE data)
        {
            var conn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            conn.Insert(data);
            try
            {
                InsertPillToSQL(data.ma_name, data.ma_desc, data.ma_set_time, data.ud_id);
            }
            catch
            {
                //pass
            }
            conn.Close();
        }

        private void InsertPillToSQL(string ma_name, string ma_desc, DateTime ma_set_time, int ud_id)
        {
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = $@"insert into med_alert values(null,'{ma_name}','{ma_desc}','{ma_set_time.ToThaiLocale().ToString("H:mm:ss")}','{ma_pic}',{ud_id})";
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
    }

}