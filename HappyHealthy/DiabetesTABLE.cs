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
using Xamarin.Forms.Platform.Android;

namespace HappyHealthyCSharp
{
    [Table("DiabetesTABLE")]
    class DiabetesTABLE
    {
        [PrimaryKey,AutoIncrement]
        public int fbs_id { get; set; }
        public DateTime fbs_time { get; set; }
        public decimal fbs_fbs { get; set; }
        public int fbs_fbs_lvl { get; set; }
        [ForeignKey(typeof(UserTABLE))]
        public int ud_id { get; set; }
        [ManyToOne]
        public UserTABLE UserTABLE { get; set; }
        //reconstruct of sqlite keys + attributes
        public DiabetesTABLE()
        {
            var sqliteConn = new SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            //var sqliteConn = new SQLiteConnection(GlobalFunction.sqliteDBPath);
            sqliteConn.CreateTable<DiabetesTABLE>();
            sqliteConn.Close();
            //constructor - no need for args since naming convention for instances variable mapping can be use : CB
        }
        public JavaList<IDictionary<string,object>> getDiabetesList(string queryCustomized = "SELECT * FROM DiabetesTABLE")
        {
            #region MySQL
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var fbsList = new JavaList<IDictionary<string, object>>();
            var query = queryCustomized;
            var tickets = new DataSet();
            var adapter = new MySqlDataAdapter(query, sqlconn);
            adapter.Fill(tickets, "FBS");
            foreach(DataRow x in tickets.Tables["FBS"].Rows)
            {
                var fbs = new JavaDictionary<string, object>();
                fbs.Add("fbs_id", GlobalFunction.StringValidation(x[0].ToString()));
                fbs.Add("fbs_time", GlobalFunction.StringValidation(x[1].ToString()));
                Console.WriteLine(GlobalFunction.StringValidation(((DateTime)x[1]).ToThaiLocale().ToString()));
                fbs.Add("fbs_fbs", GlobalFunction.StringValidation(x[2].ToString()));
                fbs.Add("fbs_fbs_lvl", GlobalFunction.StringValidation(x[3].ToString()));
                fbs.Add("ud_id", GlobalFunction.StringValidation(x[4].ToString()));
                fbsList.Add(fbs);
            }
            sqlconn.Close();
            */
#endregion
            var fbsList = new JavaList<IDictionary<string, object>>();
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(),GlobalFunction.sqliteDBPath);
            var query = queryCustomized;
            var backFromSQL = conn.Query<DiabetesTABLE>(query);
            backFromSQL.ForEach(x =>
            {
                var diabetes = new JavaDictionary<string, object>();
                diabetes.Add("fbs_id", GlobalFunction.StringValidation(x.fbs_id));
                diabetes.Add("fbs_time", GlobalFunction.StringValidation(x.fbs_time.ToLocalTime()));
                diabetes.Add("fbs_fbs", GlobalFunction.StringValidation(x.fbs_fbs));
                diabetes.Add("fbs_fbs_lvl", GlobalFunction.StringValidation(x.fbs_fbs_lvl));
                diabetes.Add("ud_id", GlobalFunction.StringValidation(x.ud_id));
                fbsList.Add(diabetes);
            });
            conn.Close();
            return fbsList;
        }
        public void deleteFbsFromSQL(string id)
        {
            /*
            var sqlconn = new MySqlConnection(GlobalFunction.remoteAccess);
            sqlconn.Open();
            var command = sqlconn.CreateCommand();
            command.CommandText = $@"DELETE FROM FBS WHERE fbs_id = {id}";
            command.ExecuteNonQuery();
            sqlconn.Close();
            */
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(),GlobalFunction.sqliteDBPath);
            conn.Delete<DiabetesTABLE>(id);
            conn.Close();
        }
        public void InsertFbsToSQL(string BloodValue,int userID)
        {
            #region deprecated
            /*
            var conn = new SQLiteAsyncConnection(GlobalFunction.dbPath);
            await conn.CreateTableAsync<FoodTABLE>();
            int retRecord = await conn.InsertAsync(foodinstance);
            */
            #endregion
            var conn = new MySqlConnection(GlobalFunction.remoteAccess);
            conn.Open();
            var sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = $@"insert into fbs values(null,'{DateTime.Now.ToThaiLocale().ToString("yyyy-MM-dd H:mm:ss")}',{BloodValue},1,{userID})";
            sqlCommand.ExecuteNonQuery();
            conn.Close();

        }
        public void InsertFbsToSQL(DiabetesTABLE data)
        {
            var conn = new SQLite.Net.SQLiteConnection(new SQLitePlatformAndroid(), GlobalFunction.sqliteDBPath);
            conn.Insert(data);
            try
            {
                InsertFbsToSQL(data.fbs_fbs.ToString(), data.ud_id);
            }
            catch
            {
                //pass
            }
            conn.Close();
        }
    }
}